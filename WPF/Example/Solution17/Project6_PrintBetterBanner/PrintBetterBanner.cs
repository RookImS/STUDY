using Project4_PrintBanner;
using Project5_ChooseFont;
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project6_PrintBetterBanner
{
    public class PrintBetterBanner : Window
    {
        TextBox txtbox;
        Typeface face;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintBetterBanner());
        }

        public PrintBetterBanner()
        {
            Title = "Print Better Banner";
            SizeToContent = SizeToContent.WidthAndHeight;

            StackPanel stack = new StackPanel();
            Content = stack;

            txtbox = new TextBox();
            txtbox.Width = 250;
            txtbox.Margin = new Thickness(12);
            stack.Children.Add(txtbox);

            Button btn = new Button();
            btn.Content = "_Font...";
            btn.Margin = new Thickness(12);
            btn.Click += FontOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            txtbox.Focus();
        }

        void FontOnClick(object sender, RoutedEventArgs args)
        {
            FontDialog dlg = new FontDialog();
            dlg.Owner = this;
            dlg.Typeface = face;

            if (dlg.ShowDialog().GetValueOrDefault())
                face = dlg.Typeface;
        }

        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if(dlg.ShowDialog().GetValueOrDefault())
            {
                PrintTicket prntkt = dlg.PrintTicket;
                prntkt.PageOrientation = PageOrientation.Portrait;
                dlg.PrintTicket = prntkt;

                BannerDocumentPaginator paginator = new BannerDocumentPaginator();

                paginator.Text = txtbox.Text;
                paginator.Typeface = face;
                paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

                dlg.PrintDocument(paginator, "Banner: " + txtbox.Text);
            }
        }
    }
}
