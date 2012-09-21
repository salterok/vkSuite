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
		DataWebContract dataContract = DataWebContract.Create();

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
			var groupDescription = new PropertyGroupDescription("Online");
			friends_list.Items.GroupDescriptions.Add(groupDescription);
			InitData();
		}

		private void InitData() {
			var auth = dataContract.GetObject("[auth]");
			if (auth == null)
				vk = new VKApiManager(2691706, AccessRights.FRIENDS, true);
			else
				vk = new VKApiManager(auth as VK.Authorization.AuthorizationDetails);
			dataContract.AddObject("[auth]", vk.AuthorizationDetails);
			var resp = vk.users.Get(new long[] {vk.AuthorizationDetails.userId}, 
				ProfileFields.Nickname | ProfileFields.PhotoMediumRec | ProfileFields.Online, null);
			var image = Helper.GetImageFromUrl(resp.GetSingleValue("user/photo_medium_rec"));
			var friendsResponse = vk.friends.Get(ProfileFields.PhotoMediumRec, NameCase.Nom);
			var friends = friendsResponse.GetObjectValues("user", "uid", "first_name", "last_name", "photo_medium_rec", "online");
			main_holder.DataContext = new {
				Nickname = resp.GetSingleValue("user/nickname"),
				PresentImage = image,
				ProfileUrl = String.Format("http://vk.com/id{0}", resp.GetSingleValue("user/uid")),
				Friends = from friend in friends
						  select new {
							  Uid = friend["uid"],
							  Name = friend["first_name"],
							  Surname = friend["last_name"],
							  Online = friend["online"],
							  Photo = Helper.GetImageFromUrl(friend["photo_medium_rec"]),
							  ProfileUrl = String.Format("http://vk.com/id{0}", friend["uid"])
						  }
			};
		}

		private void friend_img_MouseDown(object sender, MouseButtonEventArgs e) {
			System.Diagnostics.Process.Start((sender as Image).Tag as string);
		}
	}
}
