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

			var memoryStream = DataWebContract.Instance.Get(url);
			memoryStream.Seek(0, SeekOrigin.Begin);
			image.BeginInit();

			image.StreamSource = memoryStream;
			image.EndInit();

			return image;
		}
	}
}
