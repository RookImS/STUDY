using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project7_NavigateWeb
{
    public class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";

            frm = new Frame();
            Content = frm;

            Loaded += OnWindowLoaded;
        }

        void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            UriDialog dlg = new UriDialog();
            dlg.Owner = this;
            dlg.Text = "http://";
            dlg.ShowDialog();

            // Frame 컨트롤의 Source에 Uri링크를 넣음으로써 해당 내용을 가져올 수 있다.
            try
            {
                frm.Source = new Uri(dlg.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }
    }
}
