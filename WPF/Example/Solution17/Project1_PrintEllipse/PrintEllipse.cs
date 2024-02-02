using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_PrintEllipse
{
    public class PrintEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintEllipse());
        }

        public PrintEllipse()
        {
            Title = "Print Ellipse";
            FontSize = 24;

            StackPanel stack = new StackPanel();
            Content = stack;

            Button btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            // PrintDialog는 프린트에 관련된 창을 띄워준다.
            PrintDialog dlg  = new PrintDialog();

            // ShowDialog를 이용해 실제 창을 띄운다.
            // 이때, 메소드의 결과로 true, false, null을 반환한다.
            // 인쇄를 누르면 true, 취소를 누르면 false, 그 외의 닫기를 누르면 null을 반환한다.
            // GetValueOrDefault는 null을 flase로 바꿔서 if문이 정상적으로 작동할 수 있도록 해준다.
            if((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // DrawingVisual을 이용해 원하는 그래픽을 그릴 수 있다.
                DrawingVisual vis = new DrawingVisual();
                // DrawingVisual에서 RenderOpen을 호출하면 DrawingContext를 반환해준다.
                DrawingContext dc = vis.RenderOpen();

                // 반환받은 DrawingContext객체를 이용해 그래픽에 대한 설정한다.
                dc.DrawEllipse(Brushes.LightGray, new Pen(Brushes.Black, 3), 
                    new Point(dlg.PrintableAreaWidth / 2, dlg.PrintableAreaHeight / 2), 
                    dlg.PrintableAreaWidth / 2, dlg.PrintableAreaHeight / 2);
                // Close를 호출하면 설정한 그래픽을 저장한다.
                dc.Close();

                // 저장한 그래픽을 인쇄 PrintDialog객체를 이용해 인쇄한다.
                // 텍스트는 프린터 작업을 구별할 때 사용한다.
                dlg.PrintVisual(vis, "My first print job");
            }
        }
    }
}
