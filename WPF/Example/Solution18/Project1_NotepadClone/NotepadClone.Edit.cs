﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project1_NotepadClone
{
    public partial class NotepadClone
    {
        void AddEditMenu(Menu menu)
        {
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            menu.Items.Add(itemEdit);

            MenuItem itemUndo = new MenuItem();
            itemUndo.Header = "_Undo";
            itemUndo.Command = ApplicationCommands.Undo;
            itemEdit.Items.Add(itemUndo);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, UndoOnExecute, UndoCanExecute));

            MenuItem itemRedo = new MenuItem();
            itemRedo.Header = "_Redo";
            itemRedo.Command = ApplicationCommands.Redo;
            itemEdit.Items.Add(itemRedo);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, RedoOnExecute, RedoCanExecute));

            itemEdit.Items.Add(new Separator());

            MenuItem itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Command = ApplicationCommands.Cut;
            itemEdit.Items.Add(itemCut);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, CutOnExecute, CutCanExecute));

            MenuItem itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Command = ApplicationCommands.Copy;
            itemEdit.Items.Add(itemCopy);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));

            MenuItem itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Command = ApplicationCommands.Paste;
            itemEdit.Items.Add(itemPaste);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));

            MenuItem itemDel = new MenuItem();
            itemDel.Header = "De_lete";
            itemDel.Command = ApplicationCommands.Delete;
            itemEdit.Items.Add(itemDel);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));

            itemEdit.Items.Add(new Separator());

            // Find, Replace는 별개의 메소드에서 처리
            AddFindMenuItems(itemEdit);

            itemEdit.Items.Add(new Separator());

            MenuItem itemAll = new MenuItem();
            itemAll.Header = "Select _All";
            itemAll.Command = ApplicationCommands.SelectAll;
            itemEdit.Items.Add(itemAll);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, SelectAllOnExecute));

            // 커스텀 커맨드 사용
            InputGestureCollection coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F5));
            RoutedUICommand commTimeDate = new RoutedUICommand("Time/_Date", "TimeDate", GetType(), coll);

            MenuItem itemDate = new MenuItem();
            itemDate.Command = commTimeDate;
            itemEdit.Items.Add(itemDate);
            CommandBindings.Add(new CommandBinding(commTimeDate, TimeDateOnExecute));
        }

        void RedoCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = txtbox.CanRedo;
        }
        void RedoOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Redo();
        }
        void UndoCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = txtbox.CanUndo;
        }
        void UndoOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Undo();
        }
        void CutCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = txtbox.SelectedText.Length > 0;
        }
        void CutOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Cut();
        }
        void CopyOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Copy();
        }
        void DeleteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.SelectedText = "";
        }
        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Clipboard.ContainsText();
        }
        void PasteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.Paste();
        }
        void SelectAllOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.SelectAll();
        }
        void TimeDateOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            txtbox.SelectedText = DateTime.Now.ToString();
        }
    }
}
