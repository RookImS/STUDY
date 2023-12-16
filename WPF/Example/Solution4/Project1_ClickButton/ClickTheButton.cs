using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_ClickButton
{
    public class ClickTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            // Button 클래스는 Control 클래스를 상속받아 사용하고, Content 프로퍼티와 Click 이벤트가 존재한다.
            Button btn = new Button();
            // Content에 텍스트를 사용할 때, 원하는 문자에 _를 붙이면 그 문자를 단축키로 사용할 수 있다. (예시에서는 alt + c)
            btn.Content = "_Click me, please!";
            btn.Click += ButtonOnClick;
            // 원하는 요소에 임의로 포커스를 할 수 있다.
            // btn.Focus();
            // 아래와 같이 설정하면 각각 Enter와 ESC에 반응하게 만들 수 있다.
            // btn.IsDefault = true;
            // btn.IsCancel = true;

            // Margin은 해당 요소의 외부에, Padding은 내부에 여백을 만든다.
            // btn.Margin = new Thickness(96);
            // btn.Padding = new Thickness(96);

            // 내부 컨텐츠의 위치를 조정한다.
            // btn.HorizontalContentAlignment = HorizontalAlignment.Left;
            // btn.VerticalContentAlignment = VerticalAlignment.Center;

            // 외부에 대한 나의 위치를 조정한다.
            // btn.HorizontalAlignment = HorizontalAlignment.Center;
            // btn.VerticalAlignment = VerticalAlignment.Center;

            Content = btn;
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The button has been clicked and all is well.", Title);
        }
    }
}
