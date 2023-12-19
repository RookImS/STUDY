using System;
using System.Diagnostics;   // Process 클래스가 여기 있음
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project6_ExploreDirectories
{
    public class FileSystemInfoButton : Button
    {
        FileSystemInfo info;

        // 파라미터가 없는 생성자는 My Documents 버튼을 만든다.
        public FileSystemInfoButton() :
            this(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
        { }

        // 인자 1개를 받는 생성자는 디렉토리 버튼이나 파일 버튼을 만든다.
        public FileSystemInfoButton(FileSystemInfo info)
        {
            this.info = info;
            Content = info.Name;
            if(info is DirectoryInfo)
                FontWeight = FontWeights.Bold;
            Margin = new Thickness(10);
        }
        
        // 인자 2개를 받는 생성자는 상위 디렉토리 버튼을 만든다.
        public FileSystemInfoButton(FileSystemInfo info, string str) : this(info)
        {
            Content = str;
        }

        // OnClick을 오버라이딩해서 나머지 부분들을 처리
        protected override void OnClick()
        {
            if(info is FileInfo)
            {
                Process.Start(info.FullName);
            }
            else if(info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;
                Application.Current.MainWindow.Title = dir.FullName;

                Panel pnl = Parent as Panel;
                pnl.Children.Clear();

                if (dir.Parent != null)
                    pnl.Children.Add(new FileSystemInfoButton(dir.Parent, ".."));

                foreach (FileSystemInfo inf in dir.GetFileSystemInfos())
                    pnl.Children.Add(new FileSystemInfoButton(inf));
            }
            base.OnClick();
        }
    }
}
