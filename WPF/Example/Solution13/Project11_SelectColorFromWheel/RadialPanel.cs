using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project4_CircleTheButtons
{
    public class RadialPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty;

        bool showPieLines;
        double angleEach;
        Size sizeLargest;
        double radius;
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;

        static RadialPanel()
        {
            OrientationProperty = DependencyProperty.Register("Orientation",
                typeof(RadialPanelOrientation), typeof(RadialPanel),
                new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        public RadialPanelOrientation Orientation
        {
            set { SetValue(OrientationProperty, value); }
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
        }

        // 아래의 계산들로 인해서 어떤 그림이 그려지게 되는가를 살필 수 있는 선을 그릴지 결정한다.
        public bool ShowPieLines
        {
            set
            {
                if (value != showPieLines)
                    InvalidateVisual();

                showPieLines = value;
            }
            get
            {
                return showPieLines;
            }
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (InternalChildren.Count == 0)
                return new Size(0, 0);

            angleEach = 360.0 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);

            // 내부의 요소 중 가장 크기가 큰 요소들을 찾아서 이에 맞춰 원을 그린다.
            foreach(UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height, child.DesiredSize.Height);
            }

            // 설정된 방향에 따라 필요한 계산을 수행한다.
            if(Orientation == RadialPanelOrientation.ByWidth)
            {
                innerEdgeFromCenter = sizeLargest.Width / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height;

                radius = Math.Sqrt(Math.Pow(sizeLargest.Width / 2, 2) + Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                innerEdgeFromCenter = sizeLargest.Height / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;

                radius = Math.Sqrt(Math.Pow(sizeLargest.Height / 2, 2) + Math.Pow(outerEdgeFromCenter, 2));
            }

            // 필요한 공간은 각각 반지름의 2배씩되는 사각형이다.
            return new Size(2 * radius, 2 * radius);
        }

        // 계산한 값과 우리가 설정한 값에 따라서 자식들을 배치한다.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, sizeFinal.Height / 2);
            double multiplier = Math.Min(sizeFinal.Width / (2 * radius), sizeFinal.Height / (2 * radius));

            foreach(UIElement child in InternalChildren)
            {
                child.RenderTransform = Transform.Identity;

                if(Orientation == RadialPanelOrientation.ByWidth)
                {
                    child.Arrange(
                        new Rect(ptCenter.X - multiplier * sizeLargest.Width / 2, ptCenter.Y - multiplier * outerEdgeFromCenter,
                        multiplier * sizeLargest.Width, multiplier * sizeLargest.Height));
                }
                else
                {
                    child.Arrange(
                        new Rect(ptCenter.X + multiplier * innerEdgeFromCenter, ptCenter.Y - multiplier * sizeLargest.Height / 2,
                        multiplier * sizeLargest.Width, multiplier * sizeLargest.Height));
                }

                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform = new RotateTransform(angleChild, pt.X, pt.Y);

                angleChild += angleEach;
            }

            return sizeFinal;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // 선을 그리기로 설정했다면 계산된 값들에 맞춰서 해당 선을 그려준다.
            if(ShowPieLines)
            {
                Point ptCenter = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(RenderSize.Width / (2 * radius), RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                dc.DrawEllipse(null, pen, ptCenter, multiplier * radius, multiplier * radius);
                double angleChild = -angleEach / 2;

                if(Orientation == RadialPanelOrientation.ByWidth)
                    angleChild += 90;

                foreach(UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter, 
                        new Point(
                            ptCenter.X + multiplier * radius * Math.Cos(2 * Math.PI * angleChild / 360), 
                            ptCenter.Y + multiplier * radius * Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += angleEach;
                }
            }
        }
    }
}
