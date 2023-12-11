using System;
using System.Windows;
using System.Windows.Input;

namespace Project5_InheritAppWindow
{
    internal class MyWindow : Window
    {
        public MyWindow() 
        {
            Title = "Inheirt App & Window";
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);

            string strMessage =
                string.Format("Window clicked with {0} button at point ({1})", 
                args.ChangedButton, args.GetPosition(this));
            MessageBox.Show(strMessage, Title);
        }
    }
}
