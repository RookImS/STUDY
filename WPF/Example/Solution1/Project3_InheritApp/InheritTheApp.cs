using System;
using System.Windows;
using System.Windows.Input;

namespace Project3_InheritApp
{
    // Application 클래스를 상속함으로써 Application 클래스에 정의된 이벤트를 오버라이딩해서 사용할 수 있다.
    class InheritTheApp : Application
    {
        [STAThread]
        public static void Main()
        {
            InheritTheApp app = new InheritTheApp();
            app.Run();
        }

        // Run이 된 직후에 발생하는 이벤트의 핸들러
        // 이 핸들러의 args에는 Main(string[] args)와 같이 사용했을 때 받을 수 있는 문자열이 Args프로퍼티에 저장된다.
        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            Window win = new Window();
            win.Title = "Inherit the App";
            win.Show();
        }

        // 로그오프, 컴퓨터 종료 시에 발생하는 이벤트의 핸들러(윈도우즈 애플리케이션으로 컴파일해야 이벤트 발생)
        protected override void OnSessionEnding(SessionEndingCancelEventArgs args)
        {
            base.OnSessionEnding(args);

            MessageBoxResult result =
                MessageBox.Show("Do you want to save your data?",
                MainWindow.Title,               // 이 핸들러의 클래스가 Application을 상속받아 그 자체인 것이나 마찬가지이므로 내부의 값을 this를 써서 사용하거나 바로 사용 가능
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);

            // args의 Cancel프로퍼티가 true라면 윈도우가 종료되지 않는다.
            args.Cancel = (result == MessageBoxResult.Cancel);
        }
    }
}
