using System;
using System.Windows;
using System.Windows.Input;

namespace Project2_HandleEvent
{
    class HandleAnEvent
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();

            // 윈도우에 이벤트 설정
            Window win = new Window();
            win.Title = "Handle An Event";
            win.MouseDown += WindowOnMouseDown;

            app.Run(win);
        }

        // 이벤트 시 발생될 내용에 대한 메소드
        // 첫 번째 인자는 이벤트를 발생시키는 객체
        // 두 번째 인자는 이벤트와 관련된 정보를 담은 인자
        static void WindowOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            Window win = sender as Window;
            // 넘겨받은 객체를 활용하는 것이 아니라 어플리케이션 기본값에 저장된 내용을 활용할 수도 있다.
            // Window win = Application.Current.MainWindow;
            string strMessage = string.Format("Window clicked with {0} button at point ({1})", args.ChangedButton, args.GetPosition(win));
            MessageBox.Show(strMessage, win.Title);
        }
    }
}
