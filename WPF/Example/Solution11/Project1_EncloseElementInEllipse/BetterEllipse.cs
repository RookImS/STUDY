using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_RenderTheBetterEllipse
{
    public class BetterEllipse : FrameworkElement
    {
        // ellipse를 그리기 위해 필요한 의존 프로퍼티를 정의한다.
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;

        public Brush Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Brush)GetValue(FillProperty);}
        }

        public Pen Stroke
        {
            set { SetValue(StrokeProperty, value);}
            get { return (Pen)GetValue(StrokeProperty); }
        }

        static BetterEllipse()
        {
            // FillProperty는 값이 변하면 도형의 크기에는 영향을 주지 않지만 그려지는 것에는 영향을 주므로
            // 옵션으로 AffectsRender를 설정한다.
            FillProperty =
                DependencyProperty.Register("Fill", typeof(Brush), typeof(BetterEllipse),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            // StrokeProperty는 값이 변하면 도형의 크기에 영향을 주므로
            // 옵션으로 AffectsMeasure를 설정한다.
            StrokeProperty =
                DependencyProperty.Register("Stroke", typeof(Pen), typeof(BetterEllipse), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // MeasureOverride는 이 엘리먼트가 그려질 수 있는 크기를 받아서 실제로 그려질 크기를 내놓는다.
        // 이 메소드가 이용된 후에는 바로 OnRender가 실행되어 엘리먼트가 새로 그려지게 된다.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = base.MeasureOverride(sizeAvailable);

            if (Stroke != null)
                sizeDesired = new Size(Stroke.Thickness, Stroke.Thickness);

            return sizeDesired;
        }

        // 기본적으로는 그리는 것이 보류되다가 OnRender를 사용할때 비로소 그림을 그린다.
        // 이는 InvalidateVisual를 명시적으로 호출해서 사용할 수도 있다.
        protected override void OnRender(DrawingContext dc)
        {
            Size size = RenderSize;

            if(Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height - Stroke.Thickness);
            }

            dc.DrawEllipse(Fill, Stroke,
                new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                size.Width / 2, size.Height / 2);
        }
    }
}
