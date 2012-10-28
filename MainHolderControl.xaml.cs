using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vkSuite {
	/// <summary>
	/// Interaction logic for MainHolderControl.xaml
	/// </summary>
	public partial class MainHolderControl : UserControl {

		public MainHolderControl() {
			this.InitializeComponent();
			_Init();
		}

		private void _Init() {
			var groupDescription = new PropertyGroupDescription("Online");
			friends_list.Items.GroupDescriptions.Add(groupDescription);
		}

		private void friend_img_MouseDown(object sender, MouseButtonEventArgs e) {
			System.Diagnostics.Process.Start((sender as Image).Tag as string);
		}

		private void open_im_chat_MouseDown(object sender, MouseButtonEventArgs e) {
			if (Globals.chat == null || !Globals.chat.IsLoaded) {
				Globals.chat = new ChatWindow();
			};
			Globals.chat.Show();
			Globals.chat.AddCompanion(((sender as FrameworkElement).DataContext as dynamic).Uid);
		}
	}
}