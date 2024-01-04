using System;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_ListColorNames
{
    class ListColorNames : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorNames());
        }

        public ListColorNames()
        {
            Title = "LIst Color Names";

            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // 리스트박스의 아이템으로 색깔의 이름을 넣는다.
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                lstbox.Items.Add(prop.Name);
            }

            // 아래 코드를 활용하면 초기값을 설정해줄 수 있다.
            // ScrollIntoView를 이용해 해당 아이템에 대해 스크롤을 자동으로 이동시킬 수 있다.
            // lstbox.SelectedItem = "Magenta";
            // lstbox.ScrollIntoView(lstbox.SelectedItem);
            // lstbox.Focus();
        }

        // 리스트박스의 요소가 선택된 경우, 리스트박스 안에 있는 이름과 리플렉션을 이용해 Color인스턴스를 만들어 Background에 적용한다.
        void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            string str = lstbox.SelectedItem as string;
            if(str != null)
            {
                Color clr = (Color)typeof(Colors).GetProperty(str).GetValue(null, null);

                Background = new SolidColorBrush(clr);
            }
        }
    }
}
