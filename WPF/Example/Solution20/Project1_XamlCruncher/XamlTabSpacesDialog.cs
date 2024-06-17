using System;
using System.Windows;
using System.Windows.Controls;
using System.Xaml;

namespace Project1_XamlCruncher
{
    public partial class XamlTabSpacesDialog
    {
        public XamlTabSpacesDialog()
        {
            InitializeComponent();
            txtbox.Focus();
        }

        public int TabSpaces
        {
            set { txtbox.Text = value.ToString(); }
            get { return Int32.Parse(txtbox.Text); }
        }

        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;
            btnOk.IsEnabled = (Int32.TryParse(txtbox.Text, out result) && result > 0 && result < 11);
        }

        void OkOnClick(object sender, RoutedEventArgs args) 
        {
            DialogResult = true;
        }
    }
}
