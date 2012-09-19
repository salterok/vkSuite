using System.IO;
using System.Net;
using System.Text;

namespace vkSuite
{
	public class Network
	{
		public static string GetHTML(string url)
		{
			string html = null;
			HttpWebResponse response = null;
			HttpWebRequest request = null;
			TextReader reader = null;
			try
			{
				request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.AllowAutoRedirect = true;
				request.Timeout = 10000;
				request.ReadWriteTimeout = 10000;
				response = (HttpWebResponse)request.GetResponse();
				Encoding encoding;
				if (response.CharacterSet.ToLower() == "windows-1251")
				{
					encoding = Encoding.GetEncoding(1251);
				}
				else
				{
					encoding = Encoding.GetEncoding(response.CharacterSet);
				}
				reader = new StreamReader(response.GetResponseStream(), encoding);
				html = reader.ReadToEnd();
				reader.Close();
				reader.Dispose();
				response.Close();
			}
			catch (System.Exception)
			{
				return null;
			}
			return html;
		}

		public static Stream GetStream(string url)
		{
			Stream stream = null;
			WebResponse response = null;
			WebRequest request = null;
			try
			{
				request = WebRequest.Create(url);
				response = (HttpWebResponse)request.GetResponse();
				stream = response.GetResponseStream();
			}
			catch (System.Exception)
			{
				
			}
			return stream;
		}
	}
}
