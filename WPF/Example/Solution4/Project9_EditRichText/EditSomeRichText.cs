using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Project9_EditRichText
{
    public class EditSomeRichText : Window
    {
        RichTextBox txtbox;
        string strFilter =
            "Document Files(*.xaml)|*.xaml|All files (*.*)|*.*";

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeRichText());
        }

        public EditSomeRichText()
        {
            Title = "Edit Some Rich Text";

            // RichTextBox는 다양한 글자와 문단 서식이 지원된다.
            // 예를 들어 ctrl+i, ctrl+u, ctrl+b를 이용해 각각 기울임꼴, 밑줄, 굵게를 할 수 있다.
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = txtbox;

            txtbox.Focus();
        }

        // RichTextBox는 모든 키 누름을 처리해버리므로 ctrl+o, ctrl+s와 같은 명령어에 대한 기능을 넣고 싶다면
        // preview 형태로 발생하는 키입력에 대한 이벤트를 이용해서 해당 입력을 가로채서 사용한다.
        protected override void OnPreviewTextInput(TextCompositionEventArgs args)
        {
            // ctrl+o 시에는 불러오기 대화창을 연다.
            if (args.ControlText.Length > 0 && args.ControlText[0] == '\x0F')
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Filter = strFilter;

                if((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Open);
                        range.Load(strm, DataFormats.Xaml);
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }
                }
                // handled를 이용해 preview로 먼저 가로챈 입력을 실제 이벤트로서는 없는 것처럼 처리한다.
                args.Handled = true;
            }
            // ctrl+s 시에는 저장 대화창을 연다.
            if(args.ControlText.Length > 0 && args.ControlText[0] == '\x13')
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = strFilter;

                if((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtbox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Create);
                        range.Save(strm, DataFormats.Xaml);
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
                args.Handled = true;
            }
            base.OnPreviewTextInput(args);
        }
    }
}
