using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project8_EditText
{
    class EditSomeText : Window
    {
        static string strFileName = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), 
                "RookImS\\EditSomeText\\EditSomeText.txt");

        TextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit Some Text";

            // TextBox를 생성
            txtbox = new TextBox();
            // Textbox는 기본적으로 enter입력이 불가능하다.
            txtbox.AcceptsReturn = true;
            txtbox.TextWrapping = TextWrapping.Wrap;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.KeyDown += TextBoxOnKeyDown;
            Content = txtbox;

            // 텍스트 파일을 연다.
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch
            {

            }

            // TextBox의 캐럿을 지정하고, 입력 포커스를 준다.
            txtbox.CaretIndex = txtbox.Text.Length;
            txtbox.Focus();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtbox.Text);
            }

            catch(Exception exc) 
            {
                MessageBoxResult result =
                    MessageBox.Show("File could not be saved: " + exc.Message +
                    "\nClose program anyway?", Title,
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                args.Cancel = (result == MessageBoxResult.No);
            }
        }

        void TextBoxOnKeyDown(object sender, KeyEventArgs args)
        {
            // F5를 눌렀을 때 시간을 입력한다.
            if (args.Key == Key.F5)
            {
                txtbox.SelectedText = DateTime.Now.ToString();
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
            }
        }
    }
}
