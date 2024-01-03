using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project3_DiagonalizeTheButtons
{
    // Panel을 상속받지 않은 채로 새로운 종류의 Panel을 정의해본다.
    class DiagonalPanel : FrameworkElement
    {
        // UIElement 객체인 자식들을 저장할 수 있다.
        List<UIElement> children = new List<UIElement>();

        Size sizeChildrenTotal;

        public static readonly DependencyProperty BackgroundProperty;

        static DiagonalPanel()
        {
            BackgroundProperty =
                DependencyProperty.Register("Background", typeof(Brush), typeof(DiagonalPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public Brush Background
        {
            set { SetValue(BackgroundProperty, value); }
            get { return (Brush)GetValue(BackgroundProperty); }
        }

        // 지금과 같이 UIElementCollection을 사용하지 않으면
        // UIElement 객체만으로는 Visual, Logical 트리를 관리할 수 없으므로
        // 클래스 내에 전용 메소드를 만들어서 관리가 가능하게 해야한다.
        public void Add(UIElement el)
        {
            children.Add(el);
            AddVisualChild(el);
            AddLogicalChild(el);
            InvalidateMeasure();
        }
        public void Remove(UIElement el)
        {
            children.Remove(el);
            RemoveVisualChild(el);
            RemoveLogicalChild(el);
            InvalidateMeasure();
        }

        public int IndexOf(UIElement el)
        {
            return children.IndexOf(el);
        }

        // Panel에서 이미 오버라이딩된 내용들에 대해서도 오버라이딩 해야한다.
        protected override int VisualChildrenCount
        {
            get
            {
                return children.Count;
            }
        }
        protected override Visual GetVisualChild(int index)
        {
            if(index >= children.Count)
                throw new ArgumentOutOfRangeException("index");

            return children[index];
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            sizeChildrenTotal = new Size(0, 0);

            foreach(UIElement child in children)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                sizeChildrenTotal.Width += child.DesiredSize.Width;
                sizeChildrenTotal.Height += child.DesiredSize.Height;
            }

            return sizeChildrenTotal;
        }
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            Point ptChild = new Point(0, 0);

            foreach(UIElement child in children)
            {
                Size sizeChild = new Size(0, 0);

                sizeChild.Width = child.DesiredSize.Width * (sizeFinal.Width / sizeChildrenTotal.Width);
                sizeChild.Height = child.DesiredSize.Height * (sizeFinal.Height / sizeChildrenTotal.Height);

                child.Arrange(new Rect(ptChild, sizeChild));

                ptChild.X += sizeChild.Width;
                ptChild.Y += sizeChild.Height;
            }

            return sizeFinal;
        }

        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Background, null, new Rect(new Point(0, 0), RenderSize));
        }
    }
}
