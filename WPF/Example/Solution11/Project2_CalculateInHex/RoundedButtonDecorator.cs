using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_CalculateInHex
{
    // 버튼을 그리는 것에 관련된 내용들만이 담긴 클래스이다.
    // 버튼을 그리는데 필요한 각종 메소드를 담는다.(MeasureOverride, ArrangeOverride, OnRender)
    // 또한 상속받은 Decorator클래스에서 이미 Child에 대한 정의가 돼있으므로 이를 그대로 사용한다.
    // Decorator가 버튼의 실체이므로 버튼의 Child는 여기서 관리하게 된다.
    public class RoundedButtonDecorator : Decorator
    {
        public static readonly DependencyProperty IsPressedProperty;

        static RoundedButtonDecorator()
        {
            IsPressedProperty =
                DependencyProperty.Register("IsPressed", typeof(bool),
                typeof(RoundedButtonDecorator), new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public bool IsPressed
        {
            set { SetValue(IsPressedProperty, value); }
            get {  return (bool)GetValue(IsPressedProperty); }
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size szDesired = new Size(2, 2);
            sizeAvailable.Width -= 2;
            sizeAvailable.Height -= 2;

            if(Child != null)
            {
                Child.Measure(sizeAvailable);
                szDesired.Width += Child.DesiredSize.Width;
                szDesired.Height += Child.DesiredSize.Height;
            }

            return szDesired;
        }

        protected override Size ArrangeOverride(Size sizeArrange)
        {
            if(Child != null)
            {
                Point ptChild =
                    new Point(Math.Max(1, (sizeArrange.Width -
                    Child.DesiredSize.Width) / 2),
                    Math.Max(1, (sizeArrange.Height - Child.DesiredSize.Height) / 2));

                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }

            return sizeArrange;
        }

        protected override void OnRender(DrawingContext dc)
        {
            RadialGradientBrush brush = new RadialGradientBrush(
                IsPressed ? SystemColors.ControlDarkColor :
                SystemColors.ControlLightColor, SystemColors.ControlColor);

            brush.GradientOrigin = IsPressed ? new Point(0.75, 0.75) : new Point(0.25, 0.25);
            dc.DrawRoundedRectangle(brush,
                new Pen(SystemColors.ControlDarkBrush, 1),
                new Rect(new Point(0, 0), RenderSize),
                RenderSize.Height / 2, RenderSize.Height / 2);
        }
    }
}
