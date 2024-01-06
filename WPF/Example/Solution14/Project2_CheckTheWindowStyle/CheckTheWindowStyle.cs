using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_CheckTheWindowStyle
{
    public class CheckTheWindowStyle : Window
    {
        MenuItem itemchecked;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheWindowStyle());
        }

        public CheckTheWindowStyle()
        {
            Title = "Check the Window Style";

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

            MenuItem itemStyle = new MenuItem();
            itemStyle.Header = "_Style";
            menu.Items.Add(itemStyle);

            itemStyle.Items.Add(CreateMenuItem("_No border or caption", WindowStyle.None));
            itemStyle.Items.Add(CreateMenuItem("_Single-border Wnidow", WindowStyle.SingleBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("3_D- border Window", WindowStyle.ThreeDBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("_Tool Window", WindowStyle.ToolWindow));
        }

        // Checkable을 사용하지 않고, 이벤트에서 각 객체의 IsChecked를 관리함으로써 그룹 내 항목 토글을 구현한다.
        MenuItem CreateMenuItem(string str, WindowStyle style)
        {
            MenuItem item = new MenuItem();
            item.Header = str;
            item.Tag = style;
            item.IsChecked = (style == WindowStyle);
            item.Click += StyleOnClick;

            if (item.IsChecked)
                itemchecked = item;

            return item;
        }

        void StyleOnClick(object sender, RoutedEventArgs args)
        {
            itemchecked.IsChecked = false;
            itemchecked = args.Source as MenuItem;
            itemchecked.IsChecked = true;

            WindowStyle = (WindowStyle)itemchecked.Tag;
        }
    }
}
