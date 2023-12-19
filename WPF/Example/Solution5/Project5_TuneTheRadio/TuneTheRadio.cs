using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project5_TuneTheRadio
{
    public class TuneTheRadio :Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TuneTheRadio());
        }

        public TuneTheRadio()
        {
            Title = "Tune the Radio";
            SizeToContent = SizeToContent.WidthAndHeight;

            // 내부 요소를 하나의 그룹으로 묶어서 보여준다.
            GroupBox group = new GroupBox();
            group.Header = "Window Style";
            group.Margin = new Thickness(96);
            group.Padding = new Thickness(5);
            Content = group;

            StackPanel stack = new StackPanel();
            group.Content = stack;

            stack.Children.Add(CreateRadioButton("No border or caption", WindowStyle.None));
            stack.Children.Add(CreateRadioButton("Single-border window", WindowStyle.SingleBorderWindow));
            stack.Children.Add(CreateRadioButton("3D-border window", WindowStyle.ThreeDBorderWindow));
            stack.Children.Add(CreateRadioButton("Tool window", WindowStyle.ToolWindow));

            AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioOnChecked));
        }

        RadioButton CreateRadioButton(string strText, WindowStyle winstyle)
        {
            RadioButton radio = new RadioButton();
            radio.Content = strText;
            radio.Tag = winstyle;
            radio.Margin = new Thickness(5);
            radio.IsChecked = (winstyle == WindowStyle);
            // 아래의 프로퍼티를 활용하면 라디오 버튼들을 그룹으로 묶어 따로 운용할 수 있다.
            // radio.GroupName

            return radio;
        }

        void RadioOnChecked(object sender, RoutedEventArgs args)
        {
            RadioButton radio = args.Source as RadioButton;
            WindowStyle = (WindowStyle)radio.Tag;
        }
    }

}
