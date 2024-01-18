using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project3_RecurseDirectoriesIncrementally
{
    public class DirectoryTreeView : TreeView
    {
        public DirectoryTreeView()
        {
            RefreshTree();
        }

        public void RefreshTree()
        {
            BeginInit();
            Items.Clear();

            DriveInfo[] drives = DriveInfo.GetDrives();

            // 각 드라이브를 TreeView의 기초 항목으로 넣는다.
            // 이때, 각 드라이브의 특성에 따라 이미지와 이름을 결정한다.
            foreach(DriveInfo drive in drives)
            {
                char chDrive = drive.Name.ToUpper()[0];
                DirectoryTreeViewItem item = new DirectoryTreeViewItem(drive.RootDirectory);

                if (chDrive != 'A' && chDrive != 'B' &&
                    drive.IsReady && drive.VolumeLabel.Length > 0)
                    item.Text = String.Format("{0} ({1})", drive.VolumeLabel, drive.Name);
                else
                    item.Text = String.Format("{0} ({1})", drive.DriveType, drive.Name);

                if (chDrive == 'A' || chDrive == 'B')
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(new Uri("pack://application:,,/Images/35FLOPPY.BMP"));
                else if (drive.DriveType == DriveType.CDRom)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(new Uri("pack://application:,,/Images/CDDRIVE.BMP"));
                else
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(new Uri("pack://application:,,/Images/DRIVE.BMP"));

                Items.Add(item);

                // 기초 디렉토리의 바로 하위에 있는 디렉토리를 미리 찾아서 저장해둔다.
                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                    item.Populate();
            }
            EndInit();
        }
    }
}
