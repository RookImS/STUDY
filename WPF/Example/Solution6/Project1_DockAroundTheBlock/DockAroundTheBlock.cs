using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_DockAroundTheBlock
{
    class DockAroundTheBlock : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DockAroundTheBlock());
        }

        public DockAroundTheBlock()
        {
            Title = "Dock Around the Block";
            DockPanel dock = new DockPanel();
            Content = dock;

            for(int i = 0; i < 17; i++)
            {
                Button btn = new Button();
                btn.Content = "Button No. " + (i + 1);
                dock.Children.Add(btn);
                // attached property 활용
                btn.SetValue(DockPanel.DockProperty, (Dock)(i % 4));
                // 아래 코드는 같은 효과를 낸다.(static 메소드 활용)
                // DockPanel.SetDock(btn, (Dock)(i % 4));

                // 마지막 요소가 도킹 후 남은 공간을 사용하지 않게 할 수 있다.
                // dock.LastChildFill = false;
            }
        }
    }
}
