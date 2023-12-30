using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project2_DrawCircles
{
    // 이벤트의 라우팅 처리의 장점을 알 수 있는 프로그램
    // 원을 그리는 작업은 canvas의 이벤트를 활용해서 일어나고 있음
    // 만약 이미 그려진 원이 많으면 elips객체가 canvas를 가리기 때문에 원을 문제없이 추가로 그리기 위해서는
    // 일일이 원에 대한 isEnabled를 false로 바꿔서 canvas가 이벤트를 만들도록 해야함
    // 하지만 RoutedEvent는 이벤트를 전파하기 때문에 원위에서 이벤트가 일어나더라도 canvas도 이벤트가 함께 일어나 문제가 발생하지 않음
    public class DrawCircles : Window
    {
        Canvas canv;

        // 그리는 것과 관련된 필드
        bool isDrawing;
        Ellipse elips;
        Point ptCenter;

        // 드래깅과 관련된 필드
        bool isDragging;
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DrawCircles());
        }

        public DrawCircles()
        {
            Title = "Draw Cicles";
            Content = canv = new Canvas();
        }

        // 원을 그리기 시작하는 경우
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);

            if (isDragging)
                return;
            
            // 새 타원 객체 추가
            ptCenter = args.GetPosition(canv);
            elips = new Ellipse();
            elips.Stroke = SystemColors.WindowTextBrush;
            elips.StrokeThickness = 1;
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips);
            Canvas.SetLeft(elips, ptCenter.X);
            Canvas.SetTop(elips, ptCenter.Y);

            // 마우스를 캡처해 앞으로의 이벤트 준비
            CaptureMouse();
            isDrawing = true;
        }

        // 원을 옮기는 경우
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonDown(args);

            if (isDrawing)
                return;

            // 클릭한 원을 얻어오고 앞으로의 이벤트를 준비
            // 이때 캡처를 활용하지 않았으므로 이벤트가 일어나는 캔버스를 벗어났을 때, 의도치 않은 문제가 발생할 수 있음
            ptMouseStart = args.GetPosition(canv);
            elDragging = canv.InputHitTest(ptMouseStart) as FrameworkElement;

            if(elDragging != null)
            {
                ptElementStart = new Point(Canvas.GetLeft(elDragging), Canvas.GetTop(elDragging));
                isDragging = true;
            }
        }

        // 원의 색을 변경하는 경우
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);

            if(args.ChangedButton == MouseButton.Middle)
            {
                Shape shape = canv.InputHitTest(args.GetPosition(canv)) as Shape;

                if(shape != null)
                {
                    shape.Fill = (shape.Fill == Brushes.Red ? Brushes.Transparent : Brushes.Red);
                }
            }
        }

        // 마우스를 움직이는 경우에는 상황에 따라 행동이 바뀜
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            Point ptMouse = args.GetPosition(canv);

            // 그리는 경우에는 원의 크기 재조정
            if (isDrawing)
            {
                double dRadius = Math.Sqrt(Math.Pow(ptCenter.X - ptMouse.X, 2) + Math.Pow(ptCenter.Y - ptMouse.Y, 2));

                Canvas.SetLeft(elips, ptCenter.X - dRadius);
                Canvas.SetTop(elips, ptCenter.Y - dRadius);
                elips.Width = 2 * dRadius;
                elips.Height = 2 * dRadius;
            }
            // 이동하는 경우에는 원의 위치 이동
            else if(isDragging)
            {
                Canvas.SetLeft(elDragging, ptElementStart.X + ptMouse.X - ptMouseStart.X);
                Canvas.SetTop(elDragging, ptElementStart.Y + ptMouse.Y - ptMouseStart.Y);
            }
        }

        // 마우스를 떼는 경우에도 상황에 따라 행동이 바뀜
        protected override void OnMouseUp(MouseButtonEventArgs args)
        {
            base.OnMouseUp(args);

            // 그리는 경우였으면 원 그리기를 확정
            if(isDrawing && args.ChangedButton == MouseButton.Left)
            {
                elips.Stroke = Brushes.Blue;
                elips.StrokeThickness = Math.Min(24, elips.Width / 2);
                elips.Fill = Brushes.Red;
                isDrawing = false;
                // 기존에 잡아둔 마우스 캡처를 해제해서 오작동 방지
                ReleaseMouseCapture();
            }
            // 드래깅하는 경우에는 원의 위치는 이미 바뀌었으므로 상태만 바꿈
            else if(isDragging && args.ChangedButton == MouseButton.Right)
            {
                isDragging = false;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);

            // Esc를 누르면 마우스로 하던 행동을 취소
            if(args.Text.IndexOf('\x1B') != -1)
            {
                // 그릴때 캡처를 했던 것이 있으므로 이를 해제해서 오작동 방지
                if (isDrawing)
                    ReleaseMouseCapture();
                else if(isDragging)
                {
                    Canvas.SetLeft(elDragging, ptElementStart.X);
                    Canvas.SetTop(elDragging, ptElementStart.Y);
                    isDragging = false;
                }
            }
        }

        // 캡처 해제했을 때 고려해야하는 것을 처리
        protected override void OnLostMouseCapture(MouseEventArgs args)
        {
            base.OnLostMouseCapture(args);

            // 캡처 해제한 시점에서 아직 그리는 도중이었다면 비정상적으로 캡처가 해제된 것이므로 원 제거
            if(isDrawing)
            {
                canv.Children.Remove(elips);
                isDrawing = false;
            }
        }
    }
}
