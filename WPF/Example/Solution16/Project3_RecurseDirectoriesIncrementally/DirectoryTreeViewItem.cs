using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Project3_RecurseDirectoriesIncrementally
{
    public class DirectoryTreeViewItem : ImagedTreeViewItem
    {
        DirectoryInfo dir;

        public DirectoryTreeViewItem(DirectoryInfo dir)
        {
            this.dir = dir;
            Text = dir.Name;

            SelectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/OPENFOLD.BMP"));
            UnselectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/CLSDFOLD.BMP"));
        }

        public DirectoryInfo DirectoryInfo 
        {
            get { return dir; }
        }

        // 현재 객체를 기준으로 하위 디렉토리를 찾아서 트리에 넣어준다.
        public void Populate()
        {
            Items.Clear();
            DirectoryInfo[] dirs;

            try
            {
                dirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach(DirectoryInfo dirChild in dirs)
                Items.Add(new DirectoryTreeViewItem(dirChild));
        }

        // 현재 디렉토리가 확장되면서 하위 디렉토리를 보여주게 되면
        // 그 하위 디렉토리가 또 선택됐을 때를 대비해 미리 Populate메소드를 사용한다.
        protected override void OnExpanded(RoutedEventArgs args)
        {
            base.OnExpanded(args);

            foreach(object obj in Items)
            {
                DirectoryTreeViewItem item = obj as DirectoryTreeViewItem;
                item.Populate();
            }
        }
    }
}
