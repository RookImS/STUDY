using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Project4_LoadXamlFile
{
    public class LoadXamlFile : Window
    {
        Frame frame;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LoadXamlFile());
        }

        public LoadXamlFile()
        {
            Title = "Load XAML File";

            DockPanel dock = new DockPanel();
            Content = dock;

            Button btn = new Button();
            btn.Content = "Open File...";
            btn.Margin = new Thickness(12);
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.Click += ButtonOnClick;
            dock.Children.Add(btn);
            DockPanel.SetDock(btn, Dock.Top);

            frame = new Frame();
            dock.Children.Add(frame);
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";

            if((bool)dlg.ShowDialog())
            {
                try
                {
                    XmlTextReader xmlreader = new XmlTextReader(dlg.FileName);

                    object obj = XamlReader.Load(xmlreader);

                    if (obj is Window)
                    {
                        Window win = obj as Window;
                        win.Owner = this;
                        win.Show();
                    }
                    else
                        frame.Content = obj;
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
            }
        }
    }
}
