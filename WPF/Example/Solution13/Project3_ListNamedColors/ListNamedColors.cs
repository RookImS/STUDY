using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project3_ListNamedColors
{
    class ListNamedColors : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }
        public ListNamedColors()
        {
            Title = "List Named Colors";

            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            lstbox.ItemsSource = NamedColor.All;
            // 현재 Item 인스턴스의 클래스인 NamedColor의 프로퍼티 중 Name, Color를 이용해서
            // 각각 항목에 보여지는 내용과 SelectedValue을 설정한다.
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Color";
        }

        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;

            // SelectedValuePath가 Color로 설정돼있으므로 SelectedValue는 NamedColor의 Color프로퍼티를 사용한다.
            if(lstbox.SelectedValue != null)
            {
                Color clr = (Color)lstbox.SelectedValue;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
