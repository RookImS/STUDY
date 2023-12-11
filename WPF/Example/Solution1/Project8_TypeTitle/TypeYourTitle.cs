using System;
using System.Windows;
using System.Windows.Input;

namespace Project8_TypeTitle
{
    class TypeYourTitle :Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TypeYourTitle());
        }

        // 키 입력을 통해서 어떤 유니코드 문자를 얻고 싶다면 OnTextInput을 오버라이드 해야한다.
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);

            if (args.Text == "\b" && Title.Length > 0)
                Title = Title.Substring(0, Title.Length - 1);
            else if (args.Text.Length > 0 && !Char.IsControl(args.Text[0]))
                Title += args.Text;
        }
    }
}
