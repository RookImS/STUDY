using System;
using System.Windows;
using System.Windows.Input;

namespace Project5_InheritAppWindow
{
    class InheritAppAndWindow
    {
        // 프로젝트5 예제에서는 app, window 전부 상속해서 만들어 썼다. 실제로는 프로젝트6처럼 사용하는 것이 일반적이다.
        [STAThread]
        public static void Main()
        {
            MyApplication app = new MyApplication();
            app.Run();
        }
    }
}
