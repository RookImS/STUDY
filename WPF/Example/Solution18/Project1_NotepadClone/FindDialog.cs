using System;
using System.Windows;

namespace Project1_NotepadClone
{
    class FindDialog : FindReplaceDialog
    {
        public FindDialog(Window owner) : base(owner)
        {
            Title = "Find";

            lblReplace.Visibility = Visibility.Collapsed;
            txtboxReplace.Visibility = Visibility.Collapsed;
            btnReplace.Visibility = Visibility.Collapsed;
            btnAll.Visibility = Visibility.Collapsed;
        }
    }
}
