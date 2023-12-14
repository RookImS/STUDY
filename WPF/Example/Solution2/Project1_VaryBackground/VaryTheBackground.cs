using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_VaryBackground
{
    public class VaryTheBackground : Window
    {
        SolidColorBrush brush = new SolidColorBrush(Colors.Black);
        // 아래와 같은 방식으로도 Brush 객체를 생성할 수 있다.
        // 다만 이 방식을 이용할 때는 Freezable 클래스의 CanFreeze 프로퍼티가 true가 되므로,
        // 아래의 마우스 이동에 대한 색 변화를 이용하려 하면 오류가 발생한다.(아래 Freezable 클래스의 설명을 참고)
        // 이를 활용해 모니터링 필요성을 줄여서 성능의 향상을 얻을 수 있다.
        // SolidColorBrush brush = Brushes.Black;
        // 위의 기본 코드와 같은 기능은 아래와 같은 코드를 통해 수행할 수 있다.
        // 즉, 이미 상태가 고정된 객체를 복사해서 다시 고정되지 않은 객체를 만들어 사용할 수 있다.
        // SolidColorBrush brush = Brushes.Black.Clone();

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new VaryTheBackground());
        }

        public VaryTheBackground()
        {
            Title = "Vary the Background";
            Width = 384;
            Height = 384;

            // 단순히 배경을 brush 클래스의 객체로 설정했을 뿐인데 변화내용이 반영된다.
            // 이는 brush 클래스가 Freezable 클래스를 상속받았기 때문이다.
            // Freezable에는 Changed라는 이벤트가 구현돼있으며, 이 이벤트는 객체에 변화가 생길때마다 발생하고 이를 이용해 배경을 다시 그릴 수 있다.
            Background = brush;
        }

        // 마우스가 움직일 때마다 일어나는 이벤트의 핸들러
        protected override void OnMouseMove(MouseEventArgs args)
        {
            // 창의 실제 크기 계산
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight - SystemParameters.CaptionHeight;

            // 창의 가운데를 기준으로 마우스의 위치에 대한 벡터 계산
            Point ptMouse = args.GetPosition(this);
            Point ptCenter = new Point(width / 2, height / 2);
            Vector vectMouse = ptMouse- ptCenter;

            // 임의의 타원 중 현재 벡터의 각도에 맞는 점의 벡터 계산
            double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
            Vector vectEllipse = new Vector(width/2 * Math.Cos(angle), height /2 * Math.Sin(angle));

            Byte byLevel = (byte)(255 * (1 - Math.Min(1, vectMouse.Length / vectEllipse.Length)));
            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;
        }
    }
}
