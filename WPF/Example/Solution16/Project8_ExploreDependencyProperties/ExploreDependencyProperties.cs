using Project5_ShowClassHierarchy;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Project8_ExploreDependencyProperties
{
    public class ExploreDependencyProperties : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDependencyProperties());
        }

        public ExploreDependencyProperties()
        {
            Title = "Explore Dependency Properties";

            Grid grid = new Grid();
            Content = grid;

            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = new GridLength(3, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            ClassHierarchyTreeView treevue = new ClassHierarchyTreeView(typeof(DependencyObject));
            grid.Children.Add(treevue);
            Grid.SetColumn(treevue, 0);

            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            DependencyPropertyListView lstvue = new DependencyPropertyListView();
            grid.Children.Add(lstvue);
            Grid.SetColumn(lstvue, 2);

            // DependencyPropertyListView에서 Type에 따른 리스트뷰 변화의 바인딩을 이미 다 해놨으므로
            // 여기서는 단순히 TypeProperty를 바인딩하기만 하면 자동으로 내부 내용이 모두 바뀐다.
            lstvue.SetBinding(DependencyPropertyListView.TypeProperty, "SelectedItem.Type");
            lstvue.DataContext = treevue;
        }
    }
}
