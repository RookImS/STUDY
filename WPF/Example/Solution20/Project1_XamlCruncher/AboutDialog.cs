using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Project1_NotepadClone
{
    class AboutDialog : Window
    {
        public AboutDialog(Window owner)
        {
            // 어셈블리를 통해서 각종 속성을 구한다.
            Assembly asmbly = Assembly.GetExecutingAssembly();

            AssemblyTitleAttribute title = (AssemblyTitleAttribute)asmbly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            string strTitle = title.Title;

            AssemblyFileVersionAttribute version = (AssemblyFileVersionAttribute)asmbly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
            string strVersion = version.Version.Substring(0, 3);

            AssemblyCopyrightAttribute copy = (AssemblyCopyrightAttribute)asmbly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
            string strCopyright = copy.Copyright;

            Title = "About " + strTitle;
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;
            Left = owner.Left + 96;
            Top = owner.Top + 96;

            StackPanel stackMain = new StackPanel();
            Content = stackMain;

            TextBlock txtblk = new TextBlock();
            txtblk.Text = strTitle + "Version " + strVersion;
            txtblk.FontFamily = new FontFamily("Time New Roman");
            txtblk.FontSize = 32;
            txtblk.FontStyle = FontStyles.Italic;
            txtblk.Margin = new Thickness(24);
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            txtblk = new TextBlock();
            txtblk.Text = strCopyright;
            txtblk.FontSize = 20;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            Run run = new Run("www.charlespetzold.com");
            Hyperlink link = new Hyperlink(run);
            link.Click += LinkOnClick;
            txtblk = new TextBlock(link);
            txtblk.FontSize = 20;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            stackMain.Children.Add(txtblk);

            Button btn = new Button();
            btn.Content = "OK";
            btn.IsDefault = true;
            btn.IsCancel = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 48;
            btn.Margin = new Thickness(24);
            btn.Click += OkOnClick;
            stackMain.Children.Add(btn);

            btn.Focus();
        }

        void LinkOnClick(object sender, RoutedEventArgs args)
        {
            Process.Start("http://www.charlespetzold.com");
        }

        void OkOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}
