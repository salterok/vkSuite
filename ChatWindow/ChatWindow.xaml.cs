using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace vkSuite {
	/// <summary>
	/// Interaction logic for ChatWindow.xaml
	/// </summary>
	public partial class ChatWindow : Window {
		private ObservableCollection<ChatPage> pages = new ObservableCollection<ChatPage>();
		public ChatWindow() {
			InitializeComponent();
		}
		public void AddCompanion(string uid) {
			var result = pages.FirstOrDefault(p => p.Uid == uid);
			if (result == null) {
				pages.Add(new ChatPage(uid));
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			chatTab.ItemsSource = pages;
		}
	}
}
