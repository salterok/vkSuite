using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace vkSuite {
	class ChatPage {
		public string Uid { get; set; }
		public ObservableCollection<VKMessage> Messages { get; set; }


		public ChatPage(string uid) {
			this.Uid = uid;
			var resp = Globals.vk.messages.GetHistory(long.Parse(uid), null, 30);
			this.Messages = new ObservableCollection<VKMessage>(VKMessage.Create(resp).Reverse());
		}
	}
}
