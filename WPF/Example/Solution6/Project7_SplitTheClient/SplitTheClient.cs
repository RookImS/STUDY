using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Project7_SplitTheClient
{
    class SplitTheClient : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SplitTheClient());
        }

        public SplitTheClient() 
        {
            Title = "Split the Client";

            // 수직 스플리터가 있는 Grid
            Grid grid1 = new Grid();
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions.Add(new ColumnDefinition());
            grid1.ColumnDefinitions[1].Width = GridLength.Auto;
            Content = grid1;

            // 수직 스플리터의 왼쪽에 있는 버튼
            Button btn = new Button();
            btn.Content = "Button No. 1";
            grid1.Children.Add(btn);
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 0);

            // 수직 스플리터
            GridSplitter split = new GridSplitter();
            split.ShowsPreview = true;      // 이동을 시작하기 전까지 이동하지 않고 위치만 보여줌
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid1.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            // 수평 스플리터가 있는 Grid
            Grid grid2 = new Grid();
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions.Add(new RowDefinition());
            grid2.RowDefinitions[1].Height = GridLength.Auto;
            grid1.Children.Add(grid2);
            Grid.SetRow(grid2, 0); 
            Grid.SetColumn(grid2, 2);

            // 수평 스플리터 위에 있는 버튼
            btn = new Button();
            btn.Content = "Button No. 2";
            grid2.Children.Add(btn);
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 0);

            // 수평 스플리터
            split = new GridSplitter();
            split.ShowsPreview = true;
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Center;
            split.Height = 6;
            grid2.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 0);

            // 수평 스플리터 아래에 있는 버튼
            btn = new Button();
            btn.Content = "Button No. 3";
            grid2.Children.Add(btn);
            Grid.SetRow(btn, 2);
            Grid.SetColumn(btn, 0);
        }
    }
}
