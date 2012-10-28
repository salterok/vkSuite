using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace vkSuite {
	internal class UserVContract : INotifyPropertyChanged {
		private const string USER_PROFILE_TEMPLATE = "http://vk.com/id{0}";

		private BitmapImage _avatar;

		public event PropertyChangedEventHandler PropertyChanged;

		public string imageUrl;

		public string Online { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Nickname { get; set; }
		public string Uid { get; set; }
		public BitmapImage Avatar {
			get {
				return _avatar;
			}
			set {
				_avatar = value;
				NotifyPropertyChanged("Avatar");
			}
		}
		public string ProfileUrl { get { return String.Format(USER_PROFILE_TEMPLATE, Uid); } }


		private void NotifyPropertyChanged(string propertyName) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
