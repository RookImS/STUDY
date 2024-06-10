using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Project3_LoadXamlWindow
{
    public class LoadXamlWindow
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();

            Uri uri = new Uri("pack://application:,,,/LoadXamlWindow.xml");
            Stream stream = Application.GetResourceStream(uri).Stream;
            Window win = XamlReader.Load(stream) as Window;

            win.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            app.Run(win);
        }

        static void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The Button labeled '" +
                (args.Source as Button).Content +
                "' has been clicked");
        }
    }
}
