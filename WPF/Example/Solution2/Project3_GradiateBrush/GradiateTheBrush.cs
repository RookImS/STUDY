using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project3_GradiateBrush
{
    public class GradiateTheBrush :Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new GradiateTheBrush());
        }

        public GradiateTheBrush()
        {
            Title = "Gradiate the Brush";

            //LinearGradientBrush brush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
            // 아래 두 코드들은  서로 같은 효과를 갖는다.
            // int angle = 0;
            // LinearGradientBrush brush = new LinearGradientBrush(Colors.Red, Colors.Blue, angle);
            // LinearGradientBrush brush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(0, 1));

            // 아래 코드를 이용하면 SpreadMethod에 따른 그라데이션 출력방법을 확인할 수 있다.
            // LinearGradientBrush brush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(0.25, 0.25));
            // brush.SpreadMethod = GradientSpreadMethod.Pad;
            // brush.SpreadMethod = GradientSpreadMethod.Reflect;
            // brush.SpreadMethod = GradientSpreadMethod.Repeat;

            // Freezable 클래스의 change 이벤트 덕분에 창의 크기가 바뀌어도 잘 반영된다.
            Background = brush;
        }
    }
}
