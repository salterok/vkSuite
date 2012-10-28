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

namespace vkSuite
{
	/// <summary>
	/// Interaction logic for ChatView.xaml
	/// </summary>
	public partial class ChatView : UserControl
	{
		DataWebContract cache = DataWebContract.Instance;

		public ChatView()
		{
			this.InitializeComponent();
		}

		private void TextBlock_Loaded_1(object sender, RoutedEventArgs e) {
			var t = (sender as TextBlock).Text;
			var img = new Image();
			img.Source = Emotions.GetEmotion("D83DDC14");
			img.Stretch = Stretch.None;
			var i = new InlineUIContainer(img);
			//i.SetValue(WidthProperty, 32d);
			//i.SetValue(HeightProperty, 32d);
			(sender as TextBlock).Inlines.Add(i);
		}
	}
}