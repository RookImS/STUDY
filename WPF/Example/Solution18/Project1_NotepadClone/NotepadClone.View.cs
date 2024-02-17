using System;
using System.Windows;
using System.Windows.Controls;

namespace Project1_NotepadClone
{
    public partial class NotepadClone
    {
        MenuItem itemStatus;

        // 스테이터스바 활성화 여부 설정
        void AddViewMenu(Menu menu)
        {
            MenuItem itemView = new MenuItem();
            itemView.Header = "_View";
            itemView.SubmenuOpened += ViewOnOpen;
            menu.Items.Add(itemView);

            itemStatus = new MenuItem();
            itemStatus.Header = "_Status Bar";
            itemStatus.IsCheckable = true;
            itemStatus.Checked += StatusOnCheck;
            itemStatus.Unchecked += StatusOnCheck;
            itemView.Items.Add(itemStatus);
        }

        void ViewOnOpen(object sender, RoutedEventArgs args)
        {
            itemStatus.IsChecked = (status.Visibility == Visibility.Visible);
        }

        void StatusOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            status.Visibility = item.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
