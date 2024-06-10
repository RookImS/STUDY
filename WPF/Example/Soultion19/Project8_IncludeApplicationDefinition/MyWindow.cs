using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project8_IncludeApplicationDefinition
{
    public partial class MyWindow : Window
    {
        public MyWindow()
        {
            InitializeComponent();
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = sender as Button;

            MessageBox.Show("The button labeled '" + btn.Content + "' has been clicked.");
        }
    }
}
