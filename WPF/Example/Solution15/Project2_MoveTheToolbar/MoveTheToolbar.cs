using System;
using System.Windows;
using System.Windows.Controls;

namespace Project2_MoveTheToolbar
{
    public class MoveTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MoveTheToolbar());
        }

        public MoveTheToolbar()
        {
            Title = "Move The Toolbar";

            DockPanel dock = new DockPanel();
            Content = dock;

            ToolBarTray trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);

            ToolBarTray trayLeft = new ToolBarTray();
            // 툴바 트레이가 수직으로 만들어지고, 자연스럽게 툴바의 내용물들도 세로로 정렬된다.
            trayLeft.Orientation = Orientation.Vertical;
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);

            TextBox txtbox = new TextBox();
            dock.Children.Add(txtbox);

            // 각 툴바 트레이에 들어있는 툴바들은 자기가 속한 트레이 내에서만 이동이 가능하다.
            for (int i = 0; i < 6; i++)
            {
                // 툴바의 헤더는 툴바의 시작점에 나타난다.
                ToolBar toolbar = new ToolBar();
                toolbar.Header = "Toolbar " + (i + 1);

                if(i <3)
                    trayTop.ToolBars.Add(toolbar);
                else
                    trayLeft.ToolBars.Add(toolbar);

                for(int j = 0; j < 6; j++)
                {
                    Button btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j);
                    toolbar.Items.Add(btn);
                }
            }
        }
    }
}
