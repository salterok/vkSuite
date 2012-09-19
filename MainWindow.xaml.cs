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
using System.Windows.Navigation;
using System.Windows.Shapes;

using VK;

namespace vkSuite {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		VKApiManager vk;

		public MainWindow() {
			InitializeComponent();
			FlipWindow();
		}

		private void FlipWindow() {
			var h = SystemParameters.WorkArea.Top;
			var width = SystemParameters.VirtualScreenWidth;

			this.Left = width - this.Width;
			this.Top = h; // height - this.Height;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			InitData();
		}

		private void InitData() {

			vk = new VKApiManager(2691706, AccessRights.FRIENDS, true);
			var resp = vk.users.Get(new long[] {vk.AuthorizationDetails.userId}, 
				ProfileFields.Nickname | ProfileFields.PhotoMediumRec | ProfileFields.Online, null);
			var image = Helper.GetImageFromUrl(resp.GetData("user/photo_medium_rec"));
			main_holder.DataContext = new {
				Nickname = resp.GetData("user/nickname"),
				PresentImage = image,
				ProfileUrl = String.Format("http://vk.com/id{0}", resp.GetData("user/uid"))
			};

		}
	}
}
