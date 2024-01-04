using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_PaintOnCanvasClone
{
    // 기존에 제공되는 Canvas와 비슷한 기능을 제공하는 클래스를 정의한다.
    public class CanvasClone : Panel
    {
        // Canvas에서는 다른 클래스를 위해 제공되는 첨부프로퍼티가 존재하므로 이와 대응 되는 의존 프로퍼티를 만들어준다.
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;

        // 여기서 정의된 의존프로퍼티는 첨부프로퍼티로 사용하기 위해서 정의한 것이므로 등록할 때, RegisterAttached를 이용한다.
        static CanvasClone()
        {
            LeftProperty = DependencyProperty.RegisterAttached("Left",
                typeof(double), typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));

            TopProperty = DependencyProperty.RegisterAttached("Top",
                typeof(double), typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }

        // 첨부프로퍼티를 다른 클래스에서 사용할 수 있도록 하기 위해서 아래의 메소드들을 제공한다.
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }
        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        // Panel을 상속한 것이므로 아래 두 메소드는 필수적으로 구현할 필요가 있다.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            foreach(UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            }
            return base.MeasureOverride(sizeAvailable);
        }

        protected override Size ArrangeOverride(Size sizeFinal)
        {
            foreach (UIElement child in InternalChildren)
                child.Arrange(new Rect(
                    new Point(GetLeft(child), GetTop(child)), child.DesiredSize));

            return sizeFinal;
        }
    }
}
