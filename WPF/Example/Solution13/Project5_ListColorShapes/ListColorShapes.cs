using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project5_ListColorShapes
{
    class ListColorShapes : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorShapes());
        }

        public ListColorShapes()
        {
            Title = "List Color Shapes";

            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            PropertyInfo[] props = typeof(Brushes).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                Ellipse elips = new Ellipse();
                elips.Width = 100;
                elips.Height = 25;
                elips.Margin = new Thickness(10, 5, 0, 5);
                elips.Fill = prop.GetValue(null, null) as Brush;
                lstbox.Items.Add(elips);
            }

            // 아래의 이벤트 핸들러를 데이터바인딩으로 대체할 수 있다.
            // lstbox.SelectedValuePath = "Fill";
            // lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            // lstbox.DataContext = this;
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;

            if (lstbox.SelectedIndex != -1)
                Background = (lstbox.SelectedItem as Shape).Fill;
        }
    }
}
