using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project4_CommandButton
{
    public class CommandTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CommandTheButton());
        }

        public CommandTheButton()
        {
            Title = "Command the Button";

            // 버튼을 생성하고, Window의 컨텐트로 설정
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;

            // 특정 버튼에 ApplicationCommands의 커맨드를 불러와서 사용할 수 있다.
            btn.Command = ApplicationCommands.Paste;
            btn.Content = ApplicationCommands.Paste.Text;
            Content = btn;


            // Command와 이벤트 핸들러의 바인딩
            // 이와 같이 특정 커맨드를 입력했을때 어떤 이벤트핸들러가 발생하도록 하는 것을 바인딩이라고 한다.
            // 지금같은 경우에는 버튼을 누르거나, ctrl+v를 사용해서 붙여넣기가 실행되도록 할 수 있고, 바인딩에 의해 아래의 이벤트핸들러들이 실행된다.
            // 또한 PasteCanExecute와 같이 바인딩을 할 때, 유효성 검사를 해서 이벤트핸들러가 실행될지 말지를 결정할 수도 있다.
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
        }

        void PasteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            Title = Clipboard.GetText();
        }

        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Clipboard.ContainsText();
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            Title = "Command the Button";
        }
    }
}
