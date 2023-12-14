using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project8_RotateGradient
{
    public class RotateTheGradientOrigin :Window
    {
        RadialGradientBrush brush;
        double angle;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RotateTheGradientOrigin());
        }

        public RotateTheGradientOrigin()
        {
            Title = "Rotate the Gradient Origin";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 384;
            Height = 384;

            brush = new RadialGradientBrush(Colors.White, Colors.Blue);
            brush.Center = brush.GradientOrigin = new Point(0.5, 0.5);
            brush.RadiusX = brush.RadiusY = 0.1;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;
            Background = brush;

            // 경계선에 대해서도 Brush를 이용해 색을 정할 수 있고, 두께도 설정할 수 있다.
            // BorderBrush = Brushes.SaddleBrown;
            // BorderBrush = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
            // BorderThickness = new Thickness(25, 50, 75, 100);
            

            // 타이머의 Interval은 Tick 이벤트가 일어나는 주기를 설정한다.
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(100);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        void TimerOnTick(object sender, EventArgs args)
        {
            Point pt = new Point(0.5 + 0.05 * Math.Cos(angle), 0.5 + 0.05 * Math.Sin(angle));
            brush.GradientOrigin = pt;
            angle -= Math.PI / 6;
        }
    }
}
