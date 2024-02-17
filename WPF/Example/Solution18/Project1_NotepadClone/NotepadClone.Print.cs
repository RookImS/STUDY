using Project2_PrintWithMargins;
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Project1_NotepadClone
{
    public partial class NotepadClone : Window
    {
        PrintQueue prnqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        void AddPrintMenuItems(MenuItem itemFile)
        {
            MenuItem itemSetup = new MenuItem();
            itemSetup.Header = "Page Set_up...";
            itemSetup.Click += PageSetupOnClick;
            itemFile.Items.Add(itemSetup);

            MenuItem itemPrint = new MenuItem();
            itemPrint.Header = "_Print...";
            itemPrint.Command = ApplicationCommands.Print;
            itemFile.Items.Add(itemPrint);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, PrintOnExecuted));
        }

        void PageSetupOnClick(object sender, RoutedEventArgs args)
        {
            PageMarginsDialog dlg = new PageMarginsDialog();
            dlg.Owner = this;
            dlg.PageMargins = marginPage;

            if(dlg.ShowDialog().GetValueOrDefault())
            {
                marginPage = dlg.PageMargins;
            }
        }

        void PrintOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if (prnqueue != null)
                dlg.PrintQueue = prnqueue;

            if (prntkt != null)
                dlg.PrintTicket = prntkt;

            if(dlg.ShowDialog().GetValueOrDefault())
            {
                prnqueue = dlg.PrintQueue;
                prntkt = dlg.PrintTicket;

                PlainTextDocumentPaginator paginator = new PlainTextDocumentPaginator();

                paginator.PrintTicket = prntkt;
                paginator.Text = txtbox.Text;
                paginator.Header = strLoadedFile;
                paginator.Typeface = new Typeface(txtbox.FontFamily, txtbox.FontStyle, txtbox.FontWeight, txtbox.FontStretch);
                paginator.FaceSize = txtbox.FontSize;
                paginator.TextWrapping = txtbox.TextWrapping;
                paginator.Margins = marginPage;
                paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

                dlg.PrintDocument(paginator, Title);
            }
        }
    }
}
