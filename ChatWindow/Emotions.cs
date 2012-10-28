using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace vkSuite {
	static class Emotions {
		private static Dictionary<string, BitmapImage> list = null;
		private static bool state = false;
		private const string baseEmotionsUrlTemplate = "http://vk.com/images/emoji/{0}.png";
		private static IEnumerable<EmojInfo> emojies = null;
		private static string[] tokens;

		static Emotions() {
			var ser = new XmlSerializer(typeof(IEnumerable<EmojInfo>));
			var xml = new XmlDocument();
			xml.LoadXml(Properties.Resources.emoji);
			emojies = ser.Deserialize(new MemoryStream(Encoding.Unicode.GetBytes(xml.DocumentElement.SelectNodes("emoji").ToString()))) as IEnumerable<EmojInfo>;


			//tokens = new string[] { "D83DDE0A", "D83DDE03", "D83DDE09", "D83DDE06", "D83DDE1C", "D83DDE0B", "D83CDF1F",
			//"D83DDE0D", "D83DDE0E", "D83DDE12", "D83DDE0F", "D83DDE14", "D83DDE22", "D83DDE2D", "D83DDE29", "D83DDE28",
			//"D83DDE10", "D83DDE0C", "D83DDE20", "D83DDE21", "D83DDE07", "D83DDE30", "D83DDE32", "D83DDE33", "D83DDE37",
			//"D83DDE1A", "D83DDE08", "2764", "D83DDC4D", "D83DDC4E", "261D", "270C", "D83DDC4C", "26BD", "26C5", 
			//"D83CDF4C", "D83CDF7A", "D83CDF7B", "D83CDF39", "D83CDF45", "D83CDF52", "D83CDF81", "D83CDF82", "D83CDF84",
			//"D83CDFC1", "D83CDFC6", "D83DDC0E", "D83DDC0F", "D83DDC1C", "D83DDC2B", "D83DDC2E", "D83DDC03", "D83DDC3B",
			//"D83DDC3C", "D83DDC05", "D83DDC13", "D83DDC18", "D83DDC94", "D83DDCAD", "D83DDC36", "D83DDC31", "D83DDC37",
			//"D83DDC11", "23F3", "26BE", "26C4", "2600", "D83CDF3A", "D83CDF3B", "D83CDF3C", "D83CDF3D", "D83CDF4A",
			//"D83CDF4B", "D83CDF4D", "D83CDF4E", "D83CDF4F", "D83CDF6D", "D83CDF37", "D83CDF38", "D83CDF46", "D83CDF49",
			//"D83CDF50", "D83CDF51", "D83CDF53", "D83CDF54", "D83CDF55", "D83CDF56", "D83CDF57", "D83CDF69", "D83CDF83",
			//"D83CDFAA", "D83CDFB1", "D83CDFB2", "D83CDFB7", "D83CDFB8", "D83CDFBE", "D83CDFC0", "D83CDFE6", "D83DDC00",
			//"D83DDC0C", "D83DDC1B", "D83DDC1D", "D83DDC1F", "D83DDC2A", "D83DDC2C", "D83DDC2D", "D83DDC3A", "D83DDC3D",
			//"D83DDC2F", "D83DDC5C", "D83DDC7B", "D83DDC14", "D83DDC23", "D83DDC24", "D83DDC40", "D83DDC42", "D83DDC43",
			//"D83DDC46", "D83DDC47", "D83DDC48", "D83DDC51", "D83DDC60", "D83DDCA1", "D83DDCA3", "D83DDCAA", "D83DDCAC",
			//"D83DDD14", "D83DDD25" };
		}

		private static void init() {
			list = new Dictionary<string, BitmapImage>();
			foreach (var token in tokens) {
				list.Add(token,
					Helper.GetImageFromUrl(String.Format(baseEmotionsUrlTemplate, token))); 
			}
			state = true;
		}

		public static BitmapImage GetEmotion(string key) {
			if (!state)
				init();
			return list[key]; // error when key not exist?
		}

		public static string[] GetTokens() {
			return tokens;
		}
	}
}
