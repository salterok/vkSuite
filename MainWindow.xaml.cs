using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using System.Threading.Tasks;
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
			this.MaxHeight = SystemParameters.VirtualScreenHeight - h;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			InitData();
		}

		private void InitData() {
			var auth = dataContract.GetObject("[auth]");
			if (auth == null)
				vk = new VKApiManager(2691706, AccessRights.FRIENDS | AccessRights.MESSAGES, true);
			else
				vk = new VKApiManager(auth as VK.Authorization.AuthorizationDetails);
			//////////////////////////////////////////////////////////////////////////
			Globals.vk = vk;
			//////////////////////////////////////////////////////////////////////////
			dataContract.AddObject("[auth]", vk.AuthorizationDetails);
			var profileFields = ProfileFields.Nickname | ProfileFields.PhotoMediumRec | ProfileFields.Online
				| ProfileFields.FirstName | ProfileFields.LastName | ProfileFields.Counters | ProfileFields.Country;
			var resp = vk.users.Get(new long[] {vk.AuthorizationDetails.userId}, profileFields, null);
			var image = Helper.GetImageFromUrl(resp.GetSingleValue("user/photo_medium_rec"));
			var friendsResponse = vk.friends.Get(ProfileFields.PhotoMediumRec, NameCase.Nom);
			var friends = friendsResponse.GetObjectValues("user", "uid", "first_name", "last_name", "photo_medium_rec", "online");
			
			main_holder.DataContext = new {
				Nickname = resp.GetSingleValue("user/nickname"),
				Avatar = image,
				ProfileUrl = String.Format("http://vk.com/id{0}", resp.GetSingleValue("user/uid")),
				Friends = from friend in friends
						  select new {
							  Uid = friend["uid"],
							  Name = friend["first_name"],
							  Surname = friend["last_name"],
							  Online = friend["online"],
							  Avatar = Helper.GetImageFromUrl(friend["photo_medium_rec"]),
							  ProfileUrl = String.Format("http://vk.com/id{0}", friend["uid"])
						  }
			};
			


			//var data = new MainHolderVContract() {
			//	CurrentUser = new UserVContract() {
			//		Uid = resp.GetSingleValue("user/uid"),
			//		Nickname = resp.GetSingleValue("user/nickname"),
			//		Name = resp.GetSingleValue("user/first_name"),
			//		Surname = resp.GetSingleValue("user/last_name"),
			//		imageUrl = resp.GetSingleValue("user/photo_medium_rec")
			//	},
			//	Friends = new ObservableCollection<UserVContract>(
			//				from friend in friends
			//				select new UserVContract {
			//				  Uid = friend["uid"],
			//				  Name = friend["first_name"],
			//				  Surname = friend["last_name"],
			//				  Online = friend["online"],
			//				  imageUrl = friend["photo_medium_rec"]
			//			  })
			//};
			//main_holder.DataContext = data;
			//data.CurrentUser.Avatar = Helper.GetImageFromUrl(data.CurrentUser.imageUrl);
		}
	}
}
