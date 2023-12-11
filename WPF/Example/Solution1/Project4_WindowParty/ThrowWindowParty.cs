using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project4_WindowParty
{
    class ThrowWindowParty : Application
    {
        [STAThread]
        public static void Main()
        {
            ThrowWindowParty app = new ThrowWindowParty();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            Window winMain = new Window();
            winMain.Title = "Main Window";
            winMain.MouseDown += WindowOnMouseDown;
            // ShutdownMode를 활용해 창이 여러 개 있을 때 작동 방식을 결정할 수 있다.
            // ShutdownMode = ShutdownMode.OnMainWindowClose;

            winMain.Show();

            for(int i = 0; i < 2; i++)
            {
                Window win = new Window();
                win.Title= "Extra Window No. " + (i + 1);

                // MainWindow 프로퍼티를 이용해 메인윈도우를 바꿀 수 있다.
                // MainWindow = win;

                // 창이 여러개 일 때, 작업표시줄 표시유무를 결정할 수 있다.
                // win.ShowInTaskbar = false;

                // 창에 대한 소유권을 설정할 수 있다. 소유자가 닫히면 함께 닫히며 항상 그 창보다 앞에서 나타난다.
                // 이러한 형태의 창을 모달리스(modeless)대화창이라고 한다.
                // win.Owner = winMain;
                win.Show();
            }
        }

        void WindowOnMouseDown(object sender, MouseButtonEventArgs args) 
        {
            Window win = new Window();
            win.Title = "Modal Dialog Box";

            // ShowDialog를 사용함으로써 모달(modal)대화창을 만들어줄 수 있다.
            // 이는 Show와 다르게 바로 반환되지 않아서 이 창에 대한 처리를 해줘야만 다른 작업을 할 수 있다.
            // 즉 모달 대화창이 있는 동안은 다른 입력을 할 수 없다.(Run의 작업이 무효화 된다.)
            win.ShowDialog();

        }
    }
}
