using System;
using System.Windows;
using System.Windows.Input;

namespace Project5_InheritAppWindow
{
    class MyApplication : Application
    {
        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);
            MyWindow win = new MyWindow();
            win.Show();
        }
    }
}
