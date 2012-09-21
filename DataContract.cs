using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;

namespace vkSuite {
	internal class DataWebContract {
		private string indexFilename;
		private string basePath;
		private NameValueCollection indexer;
		private static DataWebContract _contract;
		private static int refsCount = 0;

		public static DataWebContract Instance {
			get {
				return _contract;
			}
		}

		public static DataWebContract Create(string path = null) {
			if (_contract == null) {
				_contract = new DataWebContract();
				var pathLocation = String.IsNullOrEmpty(path) ? @"Cache\" : path;
				pathLocation = Path.IsPathRooted(pathLocation) ? pathLocation : Path.Combine(Environment.CurrentDirectory, pathLocation);
				if (!Directory.Exists(pathLocation))
					Directory.CreateDirectory(pathLocation);
				_contract.basePath = pathLocation;
				_contract._InitIndexer();
			}
			return _contract;
		}

		private DataWebContract() {
			refsCount++;
		}

		private void _InitIndexer() {
			indexFilename = Path.Combine(basePath, "index");
			var state = File.Exists(indexFilename);
			using (var indexFile = File.Open(indexFilename, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
				if (state)
					indexer = _ReadIndexFile(indexFile);
				else
					indexer = new NameValueCollection();
			}
		}

		public Stream Get(string url) {
			if (indexer.AllKeys.Contains(url))
				return new MemoryStream(File.ReadAllBytes(Path.Combine(basePath, indexer[url])));
			else {
				var stream = Network.GetStream(url);
				var filename = Path.Combine(basePath, Guid.NewGuid().ToString());
				using (var file = File.Create(filename)) {
					stream.CopyTo(file);
					file.Seek(0, SeekOrigin.Begin);
					file.CopyTo(stream = new MemoryStream());
				}
				indexer.Add(url, filename);
				stream.Seek(0, SeekOrigin.Begin);
				return stream;
			}
		}

		private NameValueCollection _ReadIndexFile(Stream indexFile) {
			var collection = new NameValueCollection();
			using (var reader = new StreamReader(indexFile)) {
				string line;
				while ((line = reader.ReadLine()) != null) {
					var parts = line.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
					if (parts.Length == 2) {
						collection.Add(parts[0], parts[1]);
					}
				}
			}
			return collection;
		}

		~DataWebContract() {
			if (--refsCount == 0) {
				using (var indexFile = File.OpenWrite(indexFilename)) {
					foreach (var key in indexer.AllKeys) {
						var builder = new StringBuilder();
						builder.AppendFormat("{0}|{1}\n", key, indexer[key]);
						var buffer = Encoding.UTF8.GetBytes(builder.ToString());
						indexFile.Write(buffer, 0, buffer.Length);
					}
				}
			}
		}

		public void AddObject(string key, object obj) {
			var formatter = new BinaryFormatter();
			string filename;
			if (indexer.AllKeys.Contains(key))
				filename = indexer[key];
			else
				filename = Path.Combine(basePath, Guid.NewGuid().ToString());
			using (var file = File.Create(filename)) {
				formatter.Serialize(file, obj);
			}
			indexer.Set(key, filename);
		}

		public object GetObject(string key) {
			if (!indexer.AllKeys.Contains(key))
				return null;
			var formatter = new BinaryFormatter();
			object retObj;
			using (var file = File.Open(indexer[key], FileMode.Open)) {
				retObj = formatter.Deserialize(file);
			}
			return retObj;
		}
	}
}
