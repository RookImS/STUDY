using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Project6_ListSystemParameters
{
    class ListSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListSystemParameters());
        }

        public ListSystemParameters()
        {
            Title = "List System Parameters";

            ListView lstvue = new ListView();
            Content = lstvue;

            // listview는 View 프로퍼티를 가지고 있으며, 이는 GridView와 같은 ViewBase로부터 파생된 클래스를 설정한다.
            GridView grdvue = new GridView();
            // 아래 코드를 생략하면 일반적인 ListBox와 같다.
            lstvue.View = grdvue;

            // GridViewColumn을 이용해 각 열에 대한 설정을하고 Columns에 이를 등록할 수 있다.
            GridViewColumn col = new GridViewColumn();
            col.Header = "Property Name";
            col.Width = 200;
            // DisplayMemeberBinding을 통해서 보여주려하는 데이터를 직접 바인딩해서 자동으로 변경되도록 할 수 있다.
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Value";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Value");
            grdvue.Columns.Add(col);

            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            foreach(PropertyInfo prop in props)
            { 
                if(prop.PropertyType != typeof(ResourceKey))
                {
                    SystemParam sysparam = new SystemParam();
                    sysparam.Name = prop.Name;
                    sysparam.Value = prop.GetValue(null, null);
                    lstvue.Items.Add(sysparam);
                }
            }
        }
    }
}
