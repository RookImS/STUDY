using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Project5_ToggleButton
{
    public class ToggleTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ToggleTheButton());
        }

        public ToggleTheButton()
        {
            Title = "Toggle the Button";

            ToggleButton btn = new ToggleButton();
            // CheckBox btn = new CheckBox();
            btn.Content = "Can _Resize";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.IsChecked = (ResizeMode == ResizeMode.CanResize);
            btn.Checked += ButtonOnChecked;
            btn.Unchecked += ButtonOnChecked;
            Content = btn;
        }

        void ButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = sender as ToggleButton;
            // IsChecked는 현재의 설정을 알려준다. 이때, 이는 bool? 자료형을 가지므로 bool 자료형으로 변환해서 사용해야한다.
            // 즉, IsChecked는 true, false, null을 가질 수 있고, 이는 IsThreeState를 사용할 때 이용하게 된다.
            ResizeMode = (bool)btn.IsChecked ? ResizeMode.CanResize : ResizeMode.NoResize;
        }

    }
}
