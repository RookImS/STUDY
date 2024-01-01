using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_CalculateInHex
{
    // decorator가 그리는 영역을 모두 맡고 있기 때문에, 버튼의 논리적인 작동에 대한 내용을 여기서 정의한다.
    // 대표적으로 버튼에서 사용하게되는 이벤트와 입력에 따른 작동을 정의한다.
    // 또한 decorator와 묶여있는 식으로 사용하게 되므로 이 클래스의 Child로 같이 묶여있는 Decorator를 설정해야 한다.
    // 이때, 버튼의 실질적인 Child는 decorator에 있고, 이 클래스를 사용하는 측에서 Child를 부르고자 하면
    // decorator가 아니라 decorator의 Child를 부르고자 하는 것이므로 이를 고려해 프로퍼티를 만든다.
    public class RoundedButton : Control
    {
        RoundedButtonDecorator decorator;

        public static readonly RoutedEvent ClickEvent;

        static RoundedButton()
        {
            ClickEvent =
                EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(RoundedButton));
        }

        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        public UIElement Child
        {
            set { decorator.Child = value; }
            get { return decorator.Child; }
        }

        public bool IsPressed
        {
            set { decorator.IsPressed = value; }
            get { return decorator.IsPressed; }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return decorator;
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            decorator.Measure(sizeAvailable);
            return decorator.DesiredSize;
        }

        protected override Size ArrangeOverride(Size sizeArrange)
        {
            decorator.Arrange(new Rect(new Point(0, 0), sizeArrange));
            return sizeArrange;
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            if (IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);
            CaptureMouse();
            IsPressed = true;
            args.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonUp(args);

            if (IsMouseCaptured)
                if (IsMouseReallyOver)
                    OnClick();

            Mouse.Capture(null);
            IsPressed = false;
            args.Handled = true;
        }

        bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return (pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight);
            }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = RoundedButton.ClickEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }

    }
}
