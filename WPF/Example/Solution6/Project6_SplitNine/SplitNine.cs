using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project6_SplitNine
{
    public class SplitNine : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SplitNine());
        }

        public SplitNine()
        {
            Title = "Split Nine";

            Grid grid = new Grid();
            Content = grid;

            for(int i = 0; i < 3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for(int x = 0; x < 3; x++)
            {
                for(int y = 0;  y < 3; y++) 
                { 
                    Button btn = new Button();
                    btn.Content = "Row " + y + " and Column " + x;
                    btn.Margin = new Thickness(10);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, y);
                    Grid.SetColumn(btn, x);
                }
            }

            // GridSplitter 클래스는 반드시 Grid의 자식이 돼야한다.
            // 그리고 다른 엘리먼트를 Grid에서 사용할 때 처럼 설정을 해줘야 한다.
            // 이때, GridSplitter는 다른 엘리먼트와 셀을 공유하므로 상황에 따라 가려질 수 있다.
            // 여백을 주거나 더 늦게 추가해서 이를 해결할 수 있다.
            GridSplitter split = new GridSplitter();
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);
            // 어색한 splitter의 모양을 수정할 수 있다.
            // Grid.SetRowSpan(split, 3);

            // splitter의 위치를 바꾸면 스플리터의 작동 방식도 바뀌게 된다.
            // split.HorizontalAlignment = HorizontalAlignment.Left;
            // split.HorizontalAlignment = HorizontalAlignment.Center;

            // splitter의 작동방식만을 바꿀 수도 있다.
            // split.ResizeBehavior = GridResizeBehavior.PreviousAndCurrent;

            // splitter를 수평으로 만들 수도 있다.
            // split.HorizontalAlignment = HorizontalAlignment.Stretch;
            // split.VerticalAlignment = VerticalAlignment.Top;
            // split.Height = 6;

            // splitter의 방향만을 바꿀 수도 있다.
            // split.HorizontalAlignment = HorizontalAlignment.Stretch;
            // split.VerticalAlignment = VerticalAlignment.Top;
            // split.ResizeDirection = GridResizeDirection.Columns;
        }
    }
}
