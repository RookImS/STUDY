using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Project3_CheckTheColor
{
    public class CheckTheColor : Window
    {
        TextBlock text;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheColor());
        }

        public CheckTheColor()
        {
            Title = "Check the Color";

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            text = new TextBlock();
            text.Text = Title;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 32;
            text.Background = SystemColors.WindowBrush;
            text.Foreground = SystemColors.WindowTextBrush;
            dock.Children.Add(text);

            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            // 하위 메뉴가 열릴 때, SubmenuOpened 이벤트가 발생한다.
            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemForeground.SubmenuOpened += ForegroundOnOpened;
            itemColor.Items.Add(itemForeground);
            FillWithColors(itemForeground, ForegroundOnClick);

            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemBackground.SubmenuOpened += BackgroundOnOpened;
            itemColor.Items.Add(itemBackground);
            FillWithColors(itemBackground, BackgroundOnClick);
        }

        void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach(PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color clr = (Color)prop.GetValue(null, null);
                int iCount = 0;

                iCount += clr.R == 0 || clr.R == 255 ? 1 : 0;
                iCount += clr.G == 0 || clr.G == 255 ? 1 : 0;
                iCount += clr.B == 0 || clr.B == 255 ? 1 : 0;

                if(clr.A == 255 && iCount > 1)
                {
                    MenuItem item = new MenuItem();
                    item.Header = "_" + prop.Name;
                    item.Tag = clr;
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }

        // 메뉴의 하위 메뉴에 있는 내용과 현재 상태를 이용해서 
        // 각 메뉴 항목의 체크 표시를 바꿔준다.
        void ForegroundOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem itemParent = sender as MenuItem;

            foreach(MenuItem item in itemParent.Items)
            {
                item.IsChecked = ((text.Foreground as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        void BackgroundOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem itemParent = sender as MenuItem;

            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked = ((text.Background as SolidColorBrush).Color == (Color)item.Tag);
            }
        }

        void ForegroundOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Foreground = new SolidColorBrush(clr);
        }

        void BackgroundOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Background = new SolidColorBrush(clr);
        }
    }
}
