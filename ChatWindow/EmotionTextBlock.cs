using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Documents;
using System.Text.RegularExpressions;

namespace vkSuite {
	class EmotionTextBlock : TextBlock {
		public static readonly DependencyProperty TextContent =
			DependencyProperty.Register("Text", typeof(String), typeof(EmotionTextBlock),
			new FrameworkPropertyMetadata(null, OnTextSourceChanged));

		public new string Text {
			get {
				return GetValue(TextContent).ToString();
			}
			set {
				SetValue(TextContent, value);
			}
		}

		private static void OnTextSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			var text = (sender as EmotionTextBlock).Text;
			var template = String.Format("({0})", String.Join("|", Emotions.GetTokens()));
			//foreach (var item in parts) {
			//	var run = new Run(item);
			//	(sender as EmotionTextBlock).Inlines.Add(run);
			//}
		}
	}
}
