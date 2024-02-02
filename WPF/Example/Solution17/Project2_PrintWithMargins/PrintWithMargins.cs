using System;
using System.Diagnostics;
using System.Globalization;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Project2_PrintWithMargins
{
    public class PrintWithMargins : Window
    {
        // PrintQueue는 현재 설정된 프린터를 저장하고,
        // PrintTicket은 해당 프린터에 설정된 값을 저장한다.
        PrintQueue prnqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintWithMargins());
        }

        public PrintWithMargins()
        {
            Title = "Print with Margins";
            FontSize = 24;

            StackPanel stack = new StackPanel();
            Content = stack;

            Button btn = new Button();
            btn.Content = "Page Set_up...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += SetupOnClick;
            stack.Children.Add(btn);

            btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        // 여백 설정 대화상자를 띄우고, 이때 이미 설정된 값을 활용한다.
        void SetupOnClick(object sender, RoutedEventArgs args)
        {
            PageMarginsDialog dlg = new PageMarginsDialog();
            dlg.Owner = this;
            dlg.PageMargins = marginPage;
            if(dlg.ShowDialog().GetValueOrDefault())
            {
                marginPage = dlg.PageMargins;
            }
        }

        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            // 이전에 사용한 설정값을 적용한다.
            if (prnqueue != null)
                dlg.PrintQueue = prnqueue;

            if (prntkt != null)
                dlg.PrintTicket = prntkt;

            if(dlg.ShowDialog().GetValueOrDefault())
            {
                // 대화상자에 새로 저장된 설정값을 저장한다.
                prnqueue = dlg.PrintQueue;
                prntkt = dlg.PrintTicket;

                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();
                Pen pn = new Pen(Brushes.Black, 1);

                // 여백의 크기만큼 작은 사각형을 그린다.
                Rect rectPage = new Rect(marginPage.Left, marginPage.Top, 
                    dlg.PrintableAreaWidth - (marginPage.Left + marginPage.Right), 
                    dlg.PrintableAreaHeight - (marginPage.Top + marginPage.Bottom));
                dc.DrawRectangle(null, pn, rectPage);

                FormattedText formtxt = new FormattedText(String.Format("Hello, Printer! {0} x {1}", dlg.PrintableAreaWidth / 96, dlg.PrintableAreaHeight / 96),
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                    new Typeface(new FontFamily("Times New Roman"), FontStyles.Italic, FontWeights.Normal, FontStretches.Normal),
                    48, Brushes.Black);

                Size sizeText = new Size(formtxt.Width, formtxt.Height);
                Point ptText = new Point(rectPage.Left + (rectPage.Width - formtxt.Width) / 2, rectPage.Top + (rectPage.Height - formtxt.Height) / 2);

                dc.DrawText(formtxt, ptText);
                dc.DrawRectangle(null, pn, new Rect(ptText, sizeText));

                dc.Close();

                dlg.PrintVisual(vis, Title);
            }
        }
    }
}
