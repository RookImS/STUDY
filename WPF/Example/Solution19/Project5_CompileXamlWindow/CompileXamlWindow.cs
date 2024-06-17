using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project5_CompileXamlWindow
{
    // XAML에서 정의한 내용을 함께 사용하기 위해 partial을 사용한다.
    public partial class CompileXamlWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CompileXamlWindow());
        }

        public CompileXamlWindow()
        {
            // XAML 파일의 내용을 연결해준다.
            InitializeComponent();

            // lstbox나 elips와 같은 필드는 XAML에서 Name속성을 이용해 정의한 것이다.
            foreach(PropertyInfo prop in typeof(Brushes).GetProperties())
                lstbox.Items.Add(prop.Name);
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = sender as Button;
            MessageBox.Show("The Button labled '" + btn.Content + "' has been clicked.");
        }

        void ListBoxOnSelection(object sender, SelectionChangedEventArgs args)
        {
            ListBox lisbox = sender as ListBox;
            string strItem = lstbox.SelectedItem as string;
            PropertyInfo prop = typeof(Brushes).GetProperty(strItem);
            elips.Fill = (Brush)prop.GetValue(null, null);
        }
    }
}
