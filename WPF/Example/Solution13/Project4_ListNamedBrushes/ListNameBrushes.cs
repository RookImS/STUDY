using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project4_ListNamedBrushes
{
    public class ListNameBrushes : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNameBrushes());
        }

        public ListNameBrushes()
        {
            Title = "LIst Named Brushes";

            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            lstbox.ItemsSource = NamedBrush.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Brush";

            // SelectedValue 의존 프로퍼티를 Window의 Background와 바인딩함으로써 불필요한 이벤트핸들러를 줄일 수 있다.
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
        }
    }
}
