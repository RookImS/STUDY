using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project4_ShapeAnEllipse
{
    class ShapeAnEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShapeAnEllipse());
        }

        // Ellipse가 상속한 FrameworElement의 프로퍼티들을 이용해 각종 설정을 할 수 있다.
        // 하지만 Ellipse객체의 클라이언트에 대한 위치 자체를 설정할 수는 없다.
        public ShapeAnEllipse()
        {
            Title = "Shape an Ellipse";

            Ellipse elips = new Ellipse();
            elips.Fill = Brushes.AliceBlue;
            elips.StrokeThickness = 24;
            elips.Stroke = new LinearGradientBrush(Colors.CadetBlue, Colors.Chocolate, new Point(1, 0), new Point(0, 1));

            elips.Width = 300;
            elips.Height = 300;

            Content = elips;

        }
    }
}
