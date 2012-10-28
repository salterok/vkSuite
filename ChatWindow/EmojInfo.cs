using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace vkSuite {
	[XmlRoot("emoji")]
	public struct EmojInfo {
		[XmlAttribute]
		public string code;
		public char value;
		public string pseudo;
	}
}
