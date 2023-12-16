using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;

namespace Project6_BindButton
{
    public class BindTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BindTheButton());
        }

        public BindTheButton()
        {
            Title = "Bind the Button";

            ToggleButton btn = new ToggleButton();
            btn.Content = "Make _Topmost";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;

            // 바인딩을 이용하면 특정 객체의 프로퍼티를 다른 객체의 프로퍼티와 연동할 수 있다.
            // 이때는 그냥 IsChecked를 사용하는 것이 아니라 IsCheckedProperty와 같은 프로퍼티를 사용해야 한다.
            // 그리고 연동할 프로퍼티를 가지고 있을 객체는 DataContext를 통해 설정해준다.
            btn.SetBinding(ToggleButton.IsCheckedProperty, "Topmost");
            btn.DataContext = this;

            // 아래 코드는 위의 바인딩에 관련된 코드를 대체할 수 있다.
            // 이는 System.Windows.Data에 정의돼있다.
            // Binding bind = new Binding("Topmost");
            // bind.Source = this;
            // btn.SetBinding(ToggleButton.IsCheckedProperty, bind);

            Content = btn;

            // 툴팁은 마우스를 해당 엘리먼트에 두고 기다리면 나오게되는 안내를 출력한다.
            ToolTip tip = new ToolTip();
            tip.Content = "Toggle the button on to make " + "the window topmost on the desktop";
            btn.ToolTip = tip;
        }
    }
}
