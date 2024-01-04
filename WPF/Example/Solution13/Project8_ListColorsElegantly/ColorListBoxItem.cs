using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project8_ListColorsElegantly
{
    // ListBoxItem을 상속받아서 해당 클래스에 존재하는 메소드와 이벤트를 활용할 수 있다.
    // ListBoxItem 클래스는 ContentControl 클래스를 상속하므로 Content를 활용해서 항목을 폭넓게 사용할 수 있다.
    class ColorListBoxItem : ListBoxItem
    {
        string str;
        Rectangle rect;
        TextBlock text;

        public ColorListBoxItem()
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            rect = new Rectangle();
            rect.Width = 16;
            rect.Height = 16;
            rect.Margin = new Thickness(2);
            rect.Stroke = SystemColors.WindowTextBrush;
            stack.Children.Add(rect);

            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        public string Text
        {
            set
            {
                str = value;
                string strSpaced = str[0].ToString();

                for (int i = 1; i < str.Length; i++)
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") + str[i].ToString();

                text.Text = strSpaced;
            }
            get { return str; }
        }

        public Color Color
        {
            set { rect.Fill = new SolidColorBrush(value); }
            get
            {
                SolidColorBrush brush = rect.Fill as SolidColorBrush;

                return brush == null ? Colors.Transparent : brush.Color;
            }
        }

        // 이 메소드들은 해당 항목이 선택되거나 해제될때 사용된다.
        // 이는 ListBoxItem으로부터 파생된 것이다.
        protected override void OnSelected(RoutedEventArgs args)
        {
            base.OnSelected(args);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs args)
        {
            base.OnUnselected(args);
            text.FontWeight = FontWeights.Regular;
        }

        public override string ToString()
        {
            return str;
        }
    }
}
