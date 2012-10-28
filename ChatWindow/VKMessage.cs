using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK;

namespace vkSuite {
	class VKMessage {
		public string Mid { get; set; }
		public string Uid { get; set; }
		public DateTime Date { get; set; }
		public string Read { get; set; }
		public string Out { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }

		public string Emoji { get; set; }


		public static IEnumerable<VKMessage> Create(VKResponseBase source) {
			var obj = source.GetObjectValues("message", "from_id", "date", "read_state", "out", "body");
			foreach (var item in obj) {
				yield return new VKMessage() {
					Uid = item["from_id"],
					Out = item["out"],
					Date = (new DateTime()).AddMilliseconds(double.Parse(item["date"])),
					Read = item["read_state"],
					Body = item["body"]
				};
			}
		}
	}
}
