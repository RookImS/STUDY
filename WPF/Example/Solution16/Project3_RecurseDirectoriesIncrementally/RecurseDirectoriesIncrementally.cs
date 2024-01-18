using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Project3_RecurseDirectoriesIncrementally
{
    class RecurseDirectoriesIncrementally : Window
    {
        StackPanel stack;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesIncrementally());
        }

        public RecurseDirectoriesIncrementally()
        {
            Title = "Recurse Directories Incrementally";

            Grid grid = new Grid();
            Content = grid;

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            DirectoryTreeView tree = new DirectoryTreeView();
            tree.SelectedItemChanged += TreeViewOnSelectedItemChanged;
            grid.Children.Add(tree);
            Grid.SetColumn(tree, 0);

            GridSplitter split = new GridSplitter();
            split.Width = 6;
            split.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, 2);

            stack = new StackPanel();
            scroll.Content = stack;
        }

        // 어떤 항목이 선택되면 그 항목 디렉토리 내의 파일들을 정리해 표시해준다.
        void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> args)
        {
            DirectoryTreeViewItem item = args.NewValue as DirectoryTreeViewItem;

            stack.Children.Clear();

            FileInfo[] infos;

            try
            {
                infos = item.DirectoryInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach(FileInfo info in infos)
            {
                TextBlock text = new TextBlock();
                text.Text = info.Name;
                stack.Children.Add(text);
            }
        }
    }
}
