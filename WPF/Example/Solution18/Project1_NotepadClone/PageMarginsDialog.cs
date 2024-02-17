using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Project2_PrintWithMargins
{
    class PageMarginsDialog : Window
    {
        enum Side
        {
            Left, Right, Top, Bottom
        }

        TextBox[] txtbox = new TextBox[4];
        Button btnOk;

        // 페이지 여백에 관련된 프로퍼티
        public Thickness PageMargins
        {
            set
            {
                txtbox[(int)Side.Left].Text =
                    (value.Left / 96).ToString("F3");
                txtbox[(int)Side.Right].Text =
                    (value.Right / 96).ToString("F3");
                txtbox[(int)Side.Top].Text =
                    (value.Top / 96).ToString("F3");
                txtbox[(int)Side.Bottom].Text =
                    (value.Bottom / 96).ToString("F3");
            }
            get
            {
                return new Thickness(
                    Double.Parse(txtbox[(int)Side.Left].Text) * 96,
                    Double.Parse(txtbox[(int)Side.Top].Text) * 96,
                    Double.Parse(txtbox[(int)Side.Right].Text) * 96,
                    Double.Parse(txtbox[(int)Side.Bottom].Text) * 96);
            }
        }

        public PageMarginsDialog()
        {
            // 대화상자에 대한 설정
            Title = "Page Setup";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            StackPanel stack = new StackPanel();
            Content = stack;

            // 그룹박스를 만들어 통일감을 준다.
            GroupBox grpbox = new GroupBox();
            grpbox.Header = "Margins (inches)";
            grpbox.Margin = new Thickness(12);
            stack.Children.Add(grpbox);

            Grid grid = new Grid();
            grid.Margin = new Thickness(6);
            grpbox.Content = grid;

            for(int i = 0; i < 2; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for(int i = 0; i < 4; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            // 각 여백 설정에 대한 라벨링을 한다.
            for(int i = 0; i < 4; i++)
            {
                Label lbl = new Label();
                lbl.Content = "_" + Enum.GetName(typeof(Side), i) + ":";
                lbl.Margin = new Thickness(6);
                lbl.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i / 2);
                Grid.SetColumn(lbl, 2 * (i % 2));

                txtbox[i] = new TextBox();
                txtbox[i].TextChanged += TextBoxOnTextChanged;
                txtbox[i].MinWidth = 48;
                txtbox[i].Margin = new Thickness(6);
                grid.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i / 2);
                Grid.SetColumn(txtbox[i], 2 * (i % 2) + 1);
            }

            UniformGrid unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 2;
            stack.Children.Add(unigrid);

            // IsDefault를 설정하면 자동으로 Enter키가 이 버튼을 클릭하는 것으로 설정된다.
            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);

            // IsCancel을 설정하면 자동으로 ESC키가 이 버튼을 클릭하는 것으로 설정된다.
            Button btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.IsCancel = true;
            btnCancel.MinWidth = 60;
            btnCancel.Margin = new Thickness(12);
            btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
            unigrid.Children.Add(btnCancel);
        }

        // 텍스트 박스 내의 내용이 유효한지 체크한다.
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;

            btnOk.IsEnabled =
                Double.TryParse(txtbox[(int)Side.Left].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Right].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Top].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Bottom].Text, out result);
        }

        // OK를 눌렀을 때, 대화상자가 닫힐 수 있도록 해준다.
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}
