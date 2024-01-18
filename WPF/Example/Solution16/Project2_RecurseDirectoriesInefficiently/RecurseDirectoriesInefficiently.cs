using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Project2_RecurseDirectoriesInefficiently
{
    public class RecurseDirectoriesInefficiently : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesInefficiently());
        }

        public RecurseDirectoriesInefficiently()
        {
            Title = "Recurse Directories Inefficiently";

            TreeView tree = new TreeView();
            Content = tree;

            // 트리 뷰의 정보로 시스템 경로를 넣는다.
            TreeViewItem item = new TreeViewItem();
            item.Header = Path.GetPathRoot(Environment.SystemDirectory);
            item.Tag = new DirectoryInfo(item.Header as string);
            tree.Items.Add(item);

            GetSubdirectories(item);
        }

        // 전달 받은 경로의 하위 디렉토리에 대해
        // 메소드를 재귀적으로 실행해 모든 디렉토리를 찾는다.
        void GetSubdirectories(TreeViewItem item)
        {
            DirectoryInfo dir = item.Tag as DirectoryInfo;
            DirectoryInfo[] subdirs;

            try
            {
                subdirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach(DirectoryInfo subdir in subdirs)
            {
                TreeViewItem subitem = new TreeViewItem();
                subitem.Header = subdir.Name;
                subitem.Tag = subdir;
                item.Items.Add(subitem);

                GetSubdirectories(subitem);
            }
        }
    }
}
