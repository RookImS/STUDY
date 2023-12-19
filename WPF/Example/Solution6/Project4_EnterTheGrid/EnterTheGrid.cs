using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project4_EnterTheGrid
{
    public class EnterTheGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EnterTheGrid());
        }

        public EnterTheGrid()
        {
            Title = "Enter the Grid";
            MinWidth = 300;
            SizeToContent = SizeToContent.WidthAndHeight;

            StackPanel stack = new StackPanel();
            Content = stack;

            Grid grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            stack.Children.Add(grid1);

            for(int i = 0; i < 5; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid1.RowDefinitions.Add(rowdef);
            }

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid1.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            grid1.ColumnDefinitions.Add(coldef);

            string[] strLabels = {"_First name:", "_Last name:",
            "_Social security number:", "_Credit card number:", "_Other personal stuff:"};

            for(int i = 0; i < strLabels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = strLabels[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid1.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);
                TextBox txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid1.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
            }

            Grid grid2 = new Grid();
            grid2.Margin = new Thickness(10);
            stack.Children.Add(grid2);

            // 행을 1개만 사용하기 때문에 행정의는 하지 않음
            // 열 정의는 기본값을 사용하는데 기본값은 star이며 값은 1로 설정돼있음
            // star인 경우에는 같이 묶인 행, 열의 값 비율에 따라 바뀐다.
            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            grid2.ColumnDefinitions.Add(new ColumnDefinition());

            Button btn = new Button();
            btn.Content = "Submit";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsDefault = true;
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn); // 기본 설정은 행, 열이 각각 0, 0이다.
            btn = new Button();
            btn.Content = "Cancel";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsCancel = true;
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn);
            Grid.SetColumn(btn, 1);

            (stack.Children[0] as Panel).Children[1].Focus();
        }
    }
}
