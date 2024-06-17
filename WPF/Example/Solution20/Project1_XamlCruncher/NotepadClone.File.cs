using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project1_NotepadClone
{
    public partial class NotepadClone : Window
    {
        // 대화상자에서 열기, 저장에 사용하는 필터
        protected string strFilter = "TExt Documents(*.txt)|*.txt|All Files(*.*)|*.*";

        void AddFileMenu(Menu menu)
        {
            // 대부분의 기능은 ApplicationCommands에 제공하는 바인딩을 이용할 수 있다.
            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Command = ApplicationCommands.New;
            itemFile.Items.Add(itemNew);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewOnExecute));

            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open...";
            itemOpen.Command = ApplicationCommands.Open;
            itemFile.Items.Add(itemOpen);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenOnExecute));

            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Command = ApplicationCommands.Save;
            itemFile.Items.Add(itemSave);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveOnExecute));

            MenuItem itemSaveAs = new MenuItem();
            itemSaveAs.Header = "Save _As...";
            itemSaveAs.Command = ApplicationCommands.SaveAs;
            itemFile.Items.Add(itemSaveAs);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsOnExecute));

            itemFile.Items.Add(new Separator());
            AddPrintMenuItems(itemFile);
            itemFile.Items.Add(new Separator());

            MenuItem itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);
        }

        protected virtual void NewOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            if (!OkToTrash())
                return;

            txtbox.Text = "";
            strLoadedFile = null;
            isFileDirty = false;
            UpdateTitle();
        }

        void OpenOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            if (!OkToTrash())
                return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strFilter;

            if((bool)dlg.ShowDialog(this))
            {
                LoadFile(dlg.FileName);
            }
        }

        void SaveOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            if (strLoadedFile == null || strLoadedFile.Length == 0)
                DisplaySaveDialog("");
            else
                SaveFile(strLoadedFile);
        }

        void SaveAsOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            DisplaySaveDialog(strLoadedFile);
        }

        bool DisplaySaveDialog(string strFileName)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;
            dlg.FileName = strFileName;

            if((bool)dlg.ShowDialog(this))
            { 
                SaveFile(dlg.FileName);
                return true;
            }
            return false;
        }

        void ExitOnClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        // 텍스트 파일 내용이 저장될 필요가 업승면 true반환
        bool OkToTrash()
        {
            if (!isFileDirty)
                return true;

            MessageBoxResult result = MessageBox.Show("The text in the file " + strLoadedFile + "has changed\n\n" + "Do you want to save the changes?",
                strAppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Cancel)
                return false;
            else if (result == MessageBoxResult.No)
                return true;
            else
            {
                if (strLoadedFile != null && strLoadedFile.Length > 0)
                    return SaveFile(strLoadedFile);

                return DisplaySaveDialog("");
            }
        }

        void LoadFile(string strFileName)
        {
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Error on File Open: " + exc.Message, strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            strLoadedFile = strFileName;
            UpdateTitle();
            txtbox.SelectionStart = 0;
            txtbox.SelectionLength = 0;
            isFileDirty = false;
        }

        bool SaveFile(string strFileName)
        {
            try
            {
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Error on File Save" + exc.Message, strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            strLoadedFile = strFileName;
            UpdateTitle();
            isFileDirty = false;

            return true;
        }
    }
}
