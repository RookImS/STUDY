using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_StackButtons
{
    class StackTenButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }

        // Panel을 Content로 활용해서 여러 요소를 Content 하나에 적절하게 넣을 수 있다.
        public StackTenButtons()
        {
            Title = "Stack Ten Buttons";
            // SizeToContent = SizeToContent.WidthAndHeight;
            // ResizeMode = ResizeMode.CanMinimize;

            StackPanel stack = new StackPanel();
            // stack.HorizontalAlignment = HorizontalAlignment.Center;
            // stack.Background = Brushes.Aquamarine;
            // stack.Margin = new Thickness(5);
            Content = stack;

            Random rand = new Random();

            for(int i = 0; i < 10; i++)
            {
                Button btn = new Button();

                btn.Name = ((char)('A' + i)).ToString();
                btn.FontSize += rand.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                btn.Click += ButtonOnClick;

                // 아래와 같이 이벤트에 대한 처리를 설정할 수도 있다.
                // 이런 경우에는 stack이 하위의 모든 버튼에 대한 클릭 이벤트를 감시하며
                // 만약에 클릭 이벤트가 발생하면 핸들러인 ButtonOnClick을 이용해 처리한다는 것이다.
                // 이런 경우에 args의 Source는 그대로 각 버튼이지만, sender는 stack이 된다.
                // stack.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick))

                // btn.HorizontalAlignment = HorizontalAlignment.Center;
                // btn.Margin = new Thickness(5);

                stack.Children.Add(btn);
            }

            // 엘리먼트의 이름을 이용해 검색을 할 수 있다.
            // 이때, 상대적으로 상위에 있는 객체에서 해당 메소드를 사용해도 해당 객체의 내부 요소를 재귀적으로 탐색해서 이를 찾아낸다.
            // Button findButton = FindName("E") as Button;

            // 아래와 같이 패널의 children을 이용해 내부의 요소에 접근할 수 있다.
            // stack.Children[5];
            // stack.Children.IndexOf(5)
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;

            MessageBox.Show("Button" + btn.Name + " has been clicked", "Button Click");
        }
    }
}
