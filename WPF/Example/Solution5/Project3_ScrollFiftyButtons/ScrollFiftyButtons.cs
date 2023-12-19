using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project3_ScrollFiftyButtons
{
    class ScrollFiftyButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ScrollFiftyButtons());
        }

        public ScrollFiftyButtons()
        {
            Title = "Scroll Fifty Buttons";
            SizeToContent = SizeToContent.Width;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            ScrollViewer scroll = new ScrollViewer();
            // ScrollBarVisibility를 이용해 스크롤에 대한 설정을 할 수 있다.
            // scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            // scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            Content = scroll;

            StackPanel stack = new StackPanel();
            stack.Margin = new Thickness(5);
            scroll.Content = stack;

            for(int i = 0; i < 50; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + (i + 1);
                btn.Content = btn.Name + " says 'Click me'";
                btn.Margin = new Thickness(5);

                stack.Children.Add(btn);
            }
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;

            // 버튼이 null인지 검사하는 이유는 이 핸들러가 최상위 윈도우의 이벤트에 대해 사용되고 있기 때문이다.
            // 윈도우에는 일반 버튼 뿐만 아니라 스크롤도 포함이 돼있고, 스크롤의 양 끝단 화살표는 버튼으로 만들어져있기 때문에 Button.ClickEvent를 발생시킬 수 있다.
            // 이런 경우에는 args의 OriginalSource는 RepeatButton이고, Source는 ScrollViewer이기 때문에 btn은 null이 된다.
            if(btn != null)
            {
                MessageBox.Show(btn.Name + " has been clicked", "Button Click");
            }
        }
    }
}
