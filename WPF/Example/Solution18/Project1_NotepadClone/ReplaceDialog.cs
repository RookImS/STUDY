using System;
using System.Windows;

namespace Project1_NotepadClone
{
    class ReplaceDialog : FindReplaceDialog
    {
        public ReplaceDialog(Window owner) : base(owner) 
        {
            Title = "Replace";

            groupDirection.Visibility = Visibility.Hidden;
        }
    }
}
