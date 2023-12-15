using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_RecordKeystrokes
{
    public class RecordKeystrokes : Window
    {
        // content의 변화에 대한 이벤트가 어떻게 일어나는지 테스트하기 위해 사용한다.
        // (1), (2) 부분의 주석을 해제하고 테스트하면 된다.
        // StringBuilder build = new StringBuilder("text");
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecordKeystrokes());
        }

        public RecordKeystrokes()
        {
            Title = "Record Keystrokes";
            Content = "";

            // (1)
            // Content = build;
        }
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            string str = Content as string;

            if (args.Text == "\b")
            {
                if (str.Length > 0)
                    str = str.Substring(0, str.Length - 1);
            }
            else
            {
                str += args.Text;
            }
            Content = str;

            // (2)
            // if(args.Text == "\b")
            // {
            //     if (build.Length > 0)
            //         build.Remove(build.Length - 1, 1);
            // }
            // else
            // {
            //     build.Append(args.Text);
            // }
            // Content = build;

            // 위의 (1), (2)를 활성화하더라도 WPF는 Content의 객체 자체는 변화가 없다고 느끼므로 제대로 내용이 반영되지 않는다.
            // 아래의 코드를 이용하면 변했다고 느끼게 되므로 제대로 반영된다.
            // Content = null;
            // Content = build;
        }
    }
}
