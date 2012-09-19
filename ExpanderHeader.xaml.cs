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
	/// Interaction logic for ExpanderHeader.xaml
	/// </summary>
	public partial class ExpanderHeader : UserControl
	{
		public ExpanderHeader()
		{
			this.InitializeComponent();
		}

		private void curr_task_img_MouseDown(object sender, MouseButtonEventArgs e) {
			var url = (this.DataContext as dynamic).ProfileUrl as string;
			System.Diagnostics.Process.Start(url);
		}
	}
}