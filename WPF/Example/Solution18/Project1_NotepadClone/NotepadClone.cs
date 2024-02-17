using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_NotepadClone
{
    public partial class NotepadClone : Window
    {
        // 프로그램과 관련된 설정 파일의 이름
        protected string strAppTitle;
        protected string strAppData;
        protected NotepadCloneSettings settings;
        protected bool isFileDirty = false;

        protected Menu menu;
        protected TextBox txtbox;
        protected StatusBar status;

        string strLoadedFile;
        StatusBarItem statLineCol;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new NotepadClone());
        }

        public NotepadClone()
        {
            Assembly asmbly = Assembly.GetExecutingAssembly();

            AssemblyTitleAttribute title = (AssemblyTitleAttribute)asmbly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            strAppTitle = title.Title;

            AssemblyProductAttribute product = (AssemblyProductAttribute)asmbly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            strAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Petzold\\" + product.Product + "\\" + product.Product + ".Settings.xml");

            DockPanel dock = new DockPanel();
            Content = dock;

            menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            statLineCol = new StatusBarItem();
            statLineCol.HorizontalAlignment = HorizontalAlignment.Right;
            status.Items.Add(statLineCol);
            DockPanel.SetDock(statLineCol, Dock.Right);

            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.AcceptsTab = true;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.TextChanged += TextBoxOnTextChanged;
            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
            dock.Children.Add(txtbox);

            AddFileMenu(menu);
            AddEditMenu(menu);
            AddFormatMenu(menu);
            AddViewMenu(menu);
            AddHelpMenu(menu);

            // 이전의에 실행되면서 저장된 설정들을 불러옴
            settings = (NotepadCloneSettings)LoadSettings();

            // 저장된 설정들을 적용
            WindowState = settings.WindowState;

            if(settings.RestoreBounds != Rect.Empty)
            {
                Left = settings.RestoreBounds.Left;
                Top = settings.RestoreBounds.Top;
                Width = settings.RestoreBounds.Width;
                Height = settings.RestoreBounds.Height;
            }

            txtbox.TextWrapping = settings.TextWrapping;
            txtbox.FontFamily = new FontFamily(settings.FontFamily);
            txtbox.FontStyle = (FontStyle) new FontStyleConverter().ConvertFromString(settings.FontStyle);
            txtbox.FontWeight = (FontWeight) new FontWeightConverter().ConvertFromString(settings.FontWeight);
            txtbox.FontStretch = (FontStretch) new FontStretchConverter().ConvertFromString(settings.FontStretch);
            txtbox.FontSize = settings.FontSize;

            Loaded += WindowOnLoaded;

            txtbox.Focus();
        }

        protected virtual object LoadSettings()
        {
            return NotepadCloneSettings.Load(typeof(NotepadCloneSettings), strAppData);
        }

        void WindowOnLoaded(object sender, RoutedEventArgs args) 
        {
            ApplicationCommands.New.Execute(null, this);

            string[] strArgs = Environment.GetCommandLineArgs();

            // 첫 번째 인자는 프로그램 이름
            if (strArgs.Length > 1)
            {
                if (File.Exists(strArgs[1]))
                {
                    LoadFile(strArgs[1]);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Cannot find the " + Path.GetFileName(strArgs[1]) + " file. \r\n\r\n" +
                        "Do you want to create a new file?", strAppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Cancel)
                        Close();

                    else if(result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            File.Create(strLoadedFile = strArgs[1]).Close();
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Error on File Creation: " + exc.Message, strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                        UpdateTitle();
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            args.Cancel = !OkToTrash();
            settings.RestoreBounds = RestoreBounds;
        }

        // settings를 저장하고 SaveSettings를 이용해 저장
        protected override void OnClosed(EventArgs args)
        {
            base.OnClosed(args);
            settings.WindowState = WindowState;
            settings.TextWrapping = txtbox.TextWrapping;

            settings.FontFamily = txtbox.FontFamily.ToString();
            settings.FontStyle = new FontStyleConverter().ConvertToString(txtbox.FontStyle);
            settings.FontWeight= new FontWeightConverter().ConvertToString(txtbox.FontWeight);
            settings.FontStretch = new FontStretchConverter().ConvertToString(txtbox.FontStretch);
            settings.FontSize = txtbox.FontSize;

            SaveSettings();
        }

        protected virtual void SaveSettings()
        {
            settings.Save(strAppData);
        }

        protected void UpdateTitle()
        {
            if (strLoadedFile == null)
                Title = "Untitled - " + strAppTitle;
            else
                Title = Path.GetFileName(strLoadedFile) + " - " + strAppTitle;
        }

        void TextBoxOnTextChanged(object sender, RoutedEventArgs args)
        {
            isFileDirty = true;
        }

        // 상태바를 갱신
        void TextBoxOnSelectionChanged(object sender, RoutedEventArgs args)
        {
            int iChar = txtbox.SelectionStart;
            int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

            if(iLine == -1)
            {
                statLineCol.Content = "";
                return;
            }

            int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
            string str = String.Format("Line {0} Col {1}", iLine + 1, iCol + 1);

            if(txtbox.SelectionLength > 0)
            {
                iChar += txtbox.SelectionLength;
                iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);
                iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
                str += String.Format(" - Line {0} Col {1}", iLine + 1, iCol + 1);
            }
            statLineCol.Content = str;
        }
    }
}
