using Project10_SelectColorFromGrid;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Project4_SelectColorFromMenuGrid
{
    public class SelectColorFromMenuGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromMenuGrid());
        }

        public SelectColorFromMenuGrid()
        {
            Title = "Select Color from Menu Grid";

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemColor.Items.Add(itemForeground);

            // 메뉴 항목의 하위 메뉴로 MenuItem이 아닌 다른 객체를 넣을 수 있다.
            // 여기서는 이전의 ColorGrid를 활용해서 메뉴 항목의 대체로 넣고, 이를 데이터바인딩으로 묶어서 상태를 바꾼다.
            ColorGridBox clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Foreground");
            clrbox.DataContext = this;
            itemForeground.Items.Add(clrbox);

            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemColor.Items.Add(itemBackground);

            clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrbox.DataContext = this;
            itemBackground.Items.Add(clrbox);
        }
    }
}
