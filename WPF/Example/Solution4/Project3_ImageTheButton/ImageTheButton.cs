using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project3_ImageTheButton
{
    public class ImageTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ImageTheButton());
        }

        public ImageTheButton()
        {
            Title = "Image the Button";

            // 프로젝트에 포함된 이미지의 속성에서 빌드작업 부분을 Resource로 바꾸어주면 아래와 같은 경로를 통해 사용가능하다.
            Uri uri = new Uri("pack://application:,,/munch.png");

            BitmapImage bitmap = new BitmapImage(uri);

            Image img = new Image();
            img.Source = bitmap;
            img.Stretch = Stretch.None;

            Button btn = new Button();
            btn.Content = img;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;

            Content = btn;
        }
    }
}
