using System;
using System.Windows;
using System.Windows.Input;

namespace Project6_InheritWindow
{
    class InheirtTheWin : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new InheirtTheWin());
        }

        InheirtTheWin()
        {
            Title = "Inherit the Win";

            // 윈도우의 창 크기를 설정할 수 있다.
            // 이를 설정하지 않으면 초기값으로 NaN이 들어가므로 실제 창 크기는 ActualWidth, ActualHeight 프로퍼티를 통해 얻어야한다.
            // ActualWidth, ActualHeight은 창이 화면에 표시된 이후에나 실제 값을 갖는다.
            // WPF에서의 크기 단위는 장치독립적 단위(device-independent units)를 사용하고, 이는 기본값으로 1/96인치 단위를 사용한다.
            // 즉, 아래의 예제는 각각 (3, 2)인치를 나타내고 있다.
            // 만약 모니터의 ppi가 96이라면 (288, 192)픽셀을 사용하고, 120이라면 (360, 240)픽셀을 사용해서 그대로 3인치, 2인치를 나타낼 것이다.
            Width = 288;
            Height = 192;

            // 초기 창의 위치를 결정한다.
            Left = 500;
            Top = 200;

            // SystemParameters의 모든 크기는 장치독립적 단위를 사용하지만 SmallIconWidth, SmallIconHeight만은 픽셀 단위를 사용한다.
            // 아래와 같이 사용해 화면의 우측하단에 창을 둘 수 있다.
            // Left = SystemParameters.PrimaryScreenWidth - Width;
            // Top = SystemParameters.PrimaryScreenHeight - Height;
            // 위 예제는 작업표시줄에 가려지는 문제가 있다.
            // 작업표시줄까지 고려하기 위해서는 WorkArea를 사용하면 된다.
            // 이때 작업표시줄의 위치는 아래뿐만 아니라 옆, 위가 될 수도 있으므로 Rect을 이용해 해당 내용이 정의돼있다.
            // Left = SystemParameters.WorkArea.Width - Width;
            // Top = SystemParameters.WorkArea.Height - Height;
        }
    }
}
