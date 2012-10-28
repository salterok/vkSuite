using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace vkSuite {
	internal class MainHolderVContract : INotifyPropertyChanged {

		private UserVContract _currentUser;
		private ObservableCollection<UserVContract> _friends;

		public event PropertyChangedEventHandler PropertyChanged;

		public UserVContract CurrentUser {
			get {
				return _currentUser;
			}
			set {
				_currentUser = value;
				_currentUser.PropertyChanged += new PropertyChangedEventHandler(NotifyPropertyChanged);
			}
		}
		public ObservableCollection<UserVContract> Friends {
			get {
				return _friends;
			}
			set {
				_friends = value;
				_friends.CollectionChanged += NotifyPropertyChanged;
			}
		}

		private void NotifyPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (PropertyChanged != null) {
				PropertyChanged(sender, new PropertyChangedEventArgs(e.PropertyName));
			}
		}

		private void NotifyPropertyChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (PropertyChanged != null) {
				PropertyChanged(sender, new PropertyChangedEventArgs("Friends"));
			}
		}
	}
}
