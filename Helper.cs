using System;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Collections.Generic;

namespace vkSuite
{
	public static class Helper
	{
		public static BitmapImage GetImageFromUrl(string url)
		{
			var image = new BitmapImage();
			int BytesToRead = 100;
			var responseStream = Network.GetStream(url);
			if (responseStream == null)
			{
				return null;
			}
			BinaryReader reader = new BinaryReader(responseStream);
			MemoryStream memoryStream = new MemoryStream();

			byte[] bytebuffer = new byte[BytesToRead];
			int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

			while (bytesRead > 0)
			{
				memoryStream.Write(bytebuffer, 0, bytesRead);
				bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
			}

			image.BeginInit();
			memoryStream.Seek(0, SeekOrigin.Begin);

			image.StreamSource = memoryStream;
			image.EndInit();

			return image;
		}
	}
}
