using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_PeruseTheMenu
{
    public class PeruseTheMenu : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PeruseTheMenu());
        }

        public PeruseTheMenu()
        {
            Title = "Peruse the Menu";

            DockPanel dock = new DockPanel();
            Content = dock;

            // 일반적으로 메뉴는 윈도우의 가장 상단에 위치한다.
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // File 메뉴 생성
            MenuItem itemFile = new MenuItem();
            // 헤더를 정의할 때, 문자열에 _를 넣음으로써 alt를 눌렀을 때의 키보드 네비게이션 기능을 활용할 수 있다.
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemNew);

            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open";
            itemOpen.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemOpen);

            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemSave);

            // Seperator를 활용해 메뉴 항목을 구분지을 수 있다.
            itemFile.Items.Add(new Separator());

            MenuItem itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);

            // Window 메뉴 생성
            MenuItem itemWindow = new MenuItem();
            itemWindow.Header = "_Window";
            menu.Items.Add(itemWindow);

            MenuItem itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.IsCheckable = true;

            // 아래 2줄의 코드를 주석처리하고 그 아래 두줄을 사용할 수 있다.
            itemTaskbar.IsChecked = ShowInTaskbar;
            itemTaskbar.Click += TaskbarOnClick;
            // itemTaskbar.SetBinding(MenuItem.IsCheckedProperty, "ShowInTaskbar");
            // itemTaskbar.DataContext = this;

            itemWindow.Items.Add(itemTaskbar);

            // IsCheckable이 true이기 때문에 항목 선택시 IsChecked가 바뀌며
            // 이에 따라 Checked, Unchecked이벤트가 발생한다.
            MenuItem itemSize = new MenuItem();
            itemSize.Header = "Size to _Content";
            itemSize.IsCheckable = true;
            itemSize.IsChecked = SizeToContent == SizeToContent.WidthAndHeight;
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            itemWindow.Items.Add(itemSize);

            // MenuItem에도 Click 이벤트가 존재해서 이를 버튼처럼 다룰 수 있다.
            MenuItem itemResize = new MenuItem();
            itemResize.Header = "_Resizable";
            itemResize.IsCheckable = true;
            itemResize.IsChecked = ResizeMode == ResizeMode.CanResize;
            itemResize.Click += ResizeOnClick;
            itemWindow.Items.Add(itemResize);

            MenuItem itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);
        }

        void UnimplementedOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            string strItem = item.Header.ToString().Replace("_", "");
            MessageBox.Show("The " + strItem + " option has not yet been implemented", Title);
        }

        void ExitOnClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        void TaskbarOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }

        void SizeOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item =sender as MenuItem;
            SizeToContent = item.IsChecked ? SizeToContent.WidthAndHeight : SizeToContent.Manual;
        }
        void ResizeOnClick(object sender, RoutedEventArgs args)
        {
            Console.WriteLine((sender as MenuItem).IsChecked);
            MenuItem item = sender as MenuItem;

            ResizeMode = item.IsChecked ? ResizeMode.CanResize : ResizeMode.NoResize;
        }

        void TopmostOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }
    }
}
