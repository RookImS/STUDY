﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project3_FormatRichText
{
    public partial class FormatRichText : Window
    {
        string[] formats =
        {
            DataFormats.Xaml, DataFormats.XamlPackage, DataFormats.Rtf,
            DataFormats.Text, DataFormats.Text
        };

        string strFilter =
            "XAML Document Files (*.xaml)|*.xaml|" +
            "XAML Package Files (*.zip)|*.zip|" +
            "Text Files (*.txt)|*.txt|" +
            "All Files (*.*)|*.*";

        void AddFileToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New, ApplicationCommands.Open,
                ApplicationCommands.Save
            };

            string[] strImages =
            {
                "NewDocumentHS.png", "openHS.png", "saveHS.png"
            };

            for(int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/" + strImages[i]));
                img.Stretch = Stretch.None;
                btn.Content = img;
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpen));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSave));
        }

        void OnNew(object sender, ExecutedRoutedEventArgs args)
        {
            FlowDocument flow = txtbox.Document;
            TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
            range.Text = "";
        }

        // 파일을 열때 필요한 작업
        void OnOpen(object sender, ExecutedRoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                FileStream strm = null;

                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Open);
                    range.Load(strm, formats[dlg.FilterIndex - 1]);
                }
                catch(Exception exc) 
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if(strm != null) 
                        strm.Close();
                }
            }
        }

        // 파일을 저장할 때 필요한 작업
        void OnSave(object sender, ExecutedRoutedEventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                FileStream strm = null;

                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Create);
                    range.Save(strm, formats[dlg.FilterIndex - 1]);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if(strm!= null)
                        strm.Close();
                }
            }
        }
    }
}
