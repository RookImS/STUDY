using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;

namespace Project1_XamlCruncher
{
    class XamlCruncher : Project1_NotepadClone.NotepadClone
    {
        Frame frameParent;
        Window win;
        StatusBarItem statusParse;  // parsing에 따른 상태를 아래 나타낸다.
        int tabspaces = 4;

        XamlCruncherSettings settingsXaml;

        XamlOrientationMenuItem itemOrientation;
        bool isSuspendParsing = false;

        [STAThread]
        public new static void Main()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new XamlCruncher());
        }

        public bool IsSuspendParsing
        {
            set { isSuspendParsing = value; }
            get { return isSuspendParsing; }
        }

        public XamlCruncher()
        {
            strFilter = "XAML Files(*.xaml)|*.xaml|All Files(*.*)|*.*";

            DockPanel dock = txtbox.Parent as DockPanel;
            dock.Children.Remove(txtbox);

            Grid grid = new Grid();

            dock.Children.Add(grid);

            for(int i = 0; i < 3; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = new GridLength(0);
                grid.RowDefinitions.Add(rowdef);

                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = new GridLength(0);
                grid.ColumnDefinitions.Add(coldef);
            }

            grid.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            grid.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);

            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Center;
            split.Height = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 0);
            Grid.SetColumnSpan(split, 3);

            split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);
            Grid.SetRowSpan(split, 3);

            frameParent = new Frame();
            frameParent.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            grid.Children.Add(frameParent);

            txtbox.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtbox);

            settingsXaml = (XamlCruncherSettings)settings;

            // XAML 메뉴에 대한 내용만들기
            MenuItem itemXaml = new MenuItem();
            itemXaml.Header = "_Xaml";
            menu.Items.Insert(menu.Items.Count - 1, itemXaml);

            // 편집기 방향 설정
            itemOrientation = new XamlOrientationMenuItem(grid, txtbox, frameParent);
            itemOrientation.Orientation = settingsXaml.Orientation;
            itemXaml.Items.Add(itemOrientation);

            // 탭 스페이스바 설정
            MenuItem itemTabs = new MenuItem();
            itemTabs.Header = "_Tab Spaces...";
            itemTabs.Click += TabSpacesOnClick;
            itemXaml.Items.Add(itemTabs);

            // 파싱 중지
            MenuItem itemNoParse = new MenuItem();
            itemNoParse.Header = "_Suspend Parsing";
            itemNoParse.IsCheckable = true;
            itemNoParse.SetBinding(MenuItem.IsCheckedProperty, "IsSuspendParsing");
            itemNoParse.DataContext = this;
            itemXaml.Items.Add(itemNoParse);

            // 파싱 재시작
            InputGestureCollection collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F6));
            RoutedUICommand commReparse = new RoutedUICommand("_Reparse", "reparse", GetType(), collGest);

            MenuItem itemReparse = new MenuItem();
            itemReparse.Command = commReparse;
            itemXaml.Items.Add(itemReparse);

            CommandBindings.Add(new CommandBinding(commReparse, ReparseOnExecuted));

            // 윈도우창 추가로 띄우기
            collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F7));
            RoutedUICommand commShowWin = new RoutedUICommand("Show _Window", "ShowWindow", GetType(), collGest);

            MenuItem itemShowWin = new MenuItem();
            itemShowWin.Command = commShowWin;
            itemXaml.Items.Add(itemShowWin);

            CommandBindings.Add(new CommandBinding(commShowWin, ShowWindowOnExecuted, ShowWindowCanExecute));

            // 저장하기
            MenuItem itemTemplate = new MenuItem();
            itemTemplate.Header = "Save as Startup _Document";
            itemTemplate.Click += NewStartupOnClick;
            itemXaml.Items.Add(itemTemplate);

            // 도움말 메뉴 만들기
            MenuItem itemXamlHelp = new MenuItem();
            itemXamlHelp.Header = "_Help...";
            itemXamlHelp.Click += HelpOnClick;
            MenuItem itemHelp = (MenuItem)menu.Items[menu.Items.Count - 1];
            itemHelp.Items.Insert(0, itemXamlHelp);

            statusParse = new StatusBarItem();
            status.Items.Insert(0, statusParse);
            status.Visibility = Visibility.Visible;

            Dispatcher.UnhandledException += DispatcherOnUnhandledException;
        }

        protected override void NewOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            base.NewOnExecute(sender, args);

            string str = ((XamlCruncherSettings)settings).StartupDocument;

            str = str.Replace("\r\n", "\n");

            str = str.Replace("\n", "\r\n");
            txtbox.Text = str;
            isFileDirty = false;
        }

        protected override object LoadSettings()
        {
            return XamlCruncherSettings.Load(typeof(XamlCruncherSettings), strAppData);
        }

        protected override void OnClosed(EventArgs args)
        {
            settingsXaml.Orientation = itemOrientation.Orientation;
            base.OnClosed(args);
        }

        protected override void SaveSettings()
        {
            ((XamlCruncherSettings)settings).Save(strAppData);
        }

        void TabSpacesOnClick(object sender, RoutedEventArgs args)
        {
            XamlTabSpacesDialog dlg = new XamlTabSpacesDialog();
            dlg.Owner = this;
            dlg.TabSpaces = settingsXaml.TabSpaces;

            if((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                settingsXaml.TabSpaces = dlg.TabSpaces;
            }
        }

        void ReparseOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            Parse();
        }

        void ShowWindowCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = (win != null);
        }

        void ShowWindowOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            if (win != null)
                win.Show();
        }

        void NewStartupOnClick(object sender, RoutedEventArgs args)
        {
            ((XamlCruncherSettings)settings).StartupDocument = txtbox.Text;
        }

        void HelpOnClick(object sender, RoutedEventArgs args)
        {
            Uri uri = new Uri("pack://application:,,,/XamlCruncherHelp.xaml");
            Stream stream = Application.GetResourceStream(uri).Stream;

            Window win = new Window();
            win.Title = "XAML Cruncher Help";
            win.Content = XamlReader.Load(stream);
            win.Show();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);

            if(args.Source == txtbox && args.Key == Key.Tab)
            {
                string strInsert = new string(' ', tabspaces);
                int iChar = txtbox.SelectionStart;
                int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

                if(iLine != -1)
                {
                    int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
                    strInsert = new string(' ', settingsXaml.TabSpaces - iCol % settingsXaml.TabSpaces);
                }

                txtbox.SelectedText = strInsert;
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
                args.Handled = true;
            }
        }

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (IsSuspendParsing)
                txtbox.Foreground = SystemColors.WindowTextBrush;
            else
                Parse();
        }

        // 텍스트 창의 내용을 파싱해서 xaml의 형식에 맞으면 결과물을 미리보기 해줌
        void Parse()
        {
            StringReader strreader = new StringReader(txtbox.Text);
            XmlTextReader xmlreader = new XmlTextReader(strreader);

            try
            {
                object obj = XamlReader.Load(xmlreader);
                txtbox.Foreground = SystemColors.WindowTextBrush;

                if(obj is Window)
                {
                    win = obj as Window;
                    statusParse.Content = "Press F7 to display Window";
                }
                else
                {
                    win = null;
                    frameParent.Content = obj;
                    statusParse.Content = "OK";
                }
            }
            catch(Exception exc) 
            {
                txtbox.Foreground = Brushes.Red;
                statusParse.Content = exc.Message;
            }
        }

        void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            statusParse.Content = "Unhandled Exception: " + args.Exception.Message;
            args.Handled = true;
        }
    }
}
