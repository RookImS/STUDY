using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project3_ShowMyFace
{
    class ShowMyFace : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShowMyFace());
        }

        // Content 프로퍼티의 동작은 UIElement를 상속받은 것과 그렇지 않은 두 그룹으로 나눌 수 있다.
        // 전자는 OnRender로 출력이 되고, 후자는 ToString으로 출력이 된다.
        // UIElement를 직접 상속받고 있는 유일한 클래스는 FrameworkElement이며, wpf의 모든 엘리먼트는 이를 상속받는다.
        // 그 중 중요한 클래스로 Image가 있다.
        public ShowMyFace()
        {
            Title = "Show My Face";

            // BitmapImage 객체를 통해 메모리에 내용을 저장한 뒤,
            Uri uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg");
            //Uri uri = new Uri(System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "Gone Fishing.bmp"));
            BitmapImage bitmap = new BitmapImage(uri);

            // Image 객체를 이용해 실제 Content로 출력할 수 있도록 만든다.
            Image img = new Image();
            img.Source = bitmap;

            // 클라이언트의 영역에 대해서 이미지가 어떻게 출력될지 설정할 수 있다.
            // img.Stretch = Stretch.Fill;
            // img.StretchDirection = StretchDirection.DownOnly;

            // FrameworkElement에서 상속받은 프로퍼티들로 위치를 변경할 수 있다.
            // img.HorizontalAlignment = HorizontalAlignment.Right;
            // img.VerticalAlignment = VerticalAlignment.Top;
            // img.Margin = new Thickness(192, 96, 48, 0);

            Content = img;
        }
    }
}
