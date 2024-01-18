using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;
using Project6_ListSystemParameters;

namespace Project7_ListSortedSystemParameters
{
    public class ListSortedSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListSortedSystemParameters());
        }

        public ListSortedSystemParameters()
        {
            Title = "List Sorted System Parameters";

            ListView lstvue = new ListView();
            Content = lstvue;

            GridView grdvue = new GridView();
            lstvue.View = grdvue;

            GridViewColumn col = new GridViewColumn();
            col.Header = "Property Name";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Value";
            col.Width = 200;
            grdvue.Columns.Add(col);

            // 열의 데이터로 보여줄 내용을 DisplayMemeberBinding을 활용하는 것이 아니라
            // 템플릿을 통해 직접 만들어서 설정할 수도 있다.
            DataTemplate template = new DataTemplate(typeof(string));
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Value"));
            template.VisualTree = factoryTextBlock;
            col.CellTemplate = template;

            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            // SortedList를 사용해 자동으로 정렬되게끔한다.
            SortedList<string, SystemParam> sortlist = new SortedList<string, SystemParam>();

            foreach(PropertyInfo prop in props)
            {
                if(prop.PropertyType != typeof(ResourceKey))
                {
                    SystemParam sysparam = new SystemParam();
                    sysparam.Name = prop.Name;
                    sysparam.Value = prop.GetValue(null, null);
                    sortlist.Add(prop.Name, sysparam);
                }
            }

            // 리스트뷰의 ItemSource를 설정해주면 자동으로 객체가 입력되고,
            // 입력된 객체의 프로퍼티를 이용해 위에서 설정한 바인딩이나 템플릿이 적용된다.
            lstvue.ItemsSource = sortlist.Values;
        }
    }
}
