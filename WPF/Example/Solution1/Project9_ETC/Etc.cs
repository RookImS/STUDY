using System;
using System.Windows;
using System.Collections;
using System.Windows.Input;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Project9_ETC
{
    class Etc : Window
    {
        List<WindowStyle> styles = new List<WindowStyle>()
            { WindowStyle.SingleBorderWindow, WindowStyle.ToolWindow, WindowStyle.None };
        int styleNo = 0;
        List<ResizeMode> resizes = new List<ResizeMode>()
            { ResizeMode.CanResize, ResizeMode.CanResizeWithGrip, ResizeMode.CanMinimize, ResizeMode.NoResize };
        int resizeNo = 0;
        bool topmost = false;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Etc());
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // 윈도우 창의 형태 결정, 만약 메뉴가 없다면 alt + space를 통해 만들 수 있다.
            if(e.Key == Key.S)
            {
                WindowStyle = styles[styleNo];
                styleNo = (styleNo + 1) % styles.Count;
            }
            if(e.Key == Key.R) 
            {
                ResizeMode = resizes[resizeNo];
                resizeNo = (resizeNo + 1) % resizes.Count;
            }
            if(e.Key == Key.T)
            {
                Topmost = topmost;
                topmost = !topmost;
            }
        }
    }
}
