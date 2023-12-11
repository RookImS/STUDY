using System;
using System.Windows;

namespace Project1_AppWindow
{
    class SayHello
    {
        [STAThread]
        public static void Main()
        {
            // 보여지는 윈도우 생성
            Window win = new Window();
            win.Title = "Say Hello";
            win.Show();

            // 실제 작동하는 app 생성
            Application app = new Application();

            // 메시지 루프 생성(메시지 루프는 사용자 입력 받는 것을 도움)
            app.Run();

            // Run메소드가 윈도우를 받아 Show메소드를 사용하는 것까지 포함
            // app.Run(win);
        }
    }

    
}
