using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Project8_ScrollCustomColors
{
    class ColorScroll : Window
    {
        ScrollBar[] scrolls = new ScrollBar[3];
        TextBlock[] txtValue = new TextBlock[3];
        Panel pnlColor;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ColorScroll());
        }

        public ColorScroll()
        {
            Title = "Color Scroll";
            Width = 500;
            Height = 300;

            // GridMain에는 수직 스플리터가 포함
            Grid gridMain = new Grid();
            Content = gridMain;

            // GridMain의 열 정의
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(200, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            gridMain.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridMain.ColumnDefinitions.Add(coldef);

            // 수직 스플리터
            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            gridMain.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            // 색을 출력하기 위해 스플리터의 오른쪽 면에 패널 생성
            pnlColor = new StackPanel();
            pnlColor.Background = new SolidColorBrush(SystemColors.WindowColor);
            gridMain.Children.Add(pnlColor);
            Grid.SetRow(pnlColor, 0);
            Grid.SetColumn(pnlColor, 2);

            // 스플리터의 왼쪽에 두 번째 그리드
            Grid grid = new Grid();
            gridMain.Children.Add(grid);
            Grid.SetRow(grid, 0);
            Grid.SetColumn(grid, 0);

            // 세 개의 행 정의(레이블, 스크롤, 레이블)
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);

            // Red, Green, Blue에 대한 세 개의 열
            for(int i = 0; i< 3; i++)
            {
                coldef = new ColumnDefinition();
                coldef.Width = new GridLength(33, GridUnitType.Star);
                grid.ColumnDefinitions.Add(coldef);
            }

            for(int i = 0; i < 3; i++)
            {
                Label lbl = new Label();
                lbl.Content = new string[] { "Red", "Green", "Blue" }[i];
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i);

                scrolls[i] = new ScrollBar();
                scrolls[i].Focusable = true;
                scrolls[i].Orientation = Orientation.Vertical;
                scrolls[i].Minimum = 0;
                scrolls[i].Maximum = 255;
                scrolls[i].SmallChange = 1;
                scrolls[i].LargeChange = 16;
                scrolls[i].ValueChanged += ScrollOnValueChanged;
                grid.Children.Add(scrolls[i]);
                Grid.SetRow(scrolls[i], 1);
                Grid.SetColumn(scrolls[i], i);

                txtValue[i] = new TextBlock();
                txtValue[i].TextAlignment = TextAlignment.Center;
                txtValue[i].HorizontalAlignment = HorizontalAlignment.Center;
                txtValue[i].Margin = new Thickness(0);
                grid.Children.Add(txtValue[i]);
                Grid.SetRow(txtValue[i], 2);
                Grid.SetColumn(txtValue[i], i);
            }

            // 스크롤바 초기화
            Color clr = (pnlColor.Background as SolidColorBrush).Color;
            scrolls[0].Value = clr.R;
            scrolls[1].Value = clr.G;
            scrolls[2].Value = clr.B;

            scrolls[0].Focus();
        }

        void ScrollOnValueChanged(object sender, EventArgs args)
        {
            ScrollBar scroll = sender as ScrollBar;
            Panel pnl = scroll.Parent as Panel;
            TextBlock txt = pnl.Children[1 + pnl.Children.IndexOf(scroll)] as TextBlock;

            txt.Text = String.Format("{0}\n0x{0:X2}", (int)scroll.Value);
            pnlColor.Background = new SolidColorBrush(Color.FromRgb((byte)scrolls[0].Value, (byte)scrolls[1].Value, (byte)scrolls[2].Value));
        }
    }
}
