using System;
using System.Windows;
using System.Windows.Input;

namespace Project7_GrowShrink
{
    class GrowAndShrink : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new GrowAndShrink());
        }

        public GrowAndShrink()
        {
            Title = "Grow & Shrink";
            Width = 192;
            Height = 192;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // 위 코드와 같은 효과를 내는 다른 방법
            // Left = (SystemParameters.WorkArea.Width - Width) / 2 + SystemParameters.WorkArea.Left;
            // Top = (SystemParameters.WorkArea.Height - Height) / 2 + SystemParameters.WorkArea.Top;
        }

        // 키 누름과 관련된 이벤트의 핸들러를 오버라이드해서 사용할 수 있다.
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if(args.Key == Key.Up) 
            {
                Left -= 0.05 * Width;
                Top -= 0.05 * Height;
                Width *= 1.1;
                Height *= 1.1;
            }
            else if (args.Key == Key.Down)
            {
                Left += 0.05 * (Width /= 1.1);
                Top += 0.05 * (Height /= 1.1);
            }
        }
    }
}
