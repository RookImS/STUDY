using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_EncloseElementInEllipse
{
    public class EllipseWithChild : Project1_RenderTheBetterEllipse.BetterEllipse
    {
        UIElement child;

        // child를 프로퍼티로 사용하면서,
        // child에 관련된 비주얼 논리 트리를 관리해준다.
        public UIElement Child
        {
            set
            {
                if(child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }

            get { return child; }
        }

        // 단일 자식을 가지는 엘리먼트이므로 아래 두 메소드와 같은 동작을 한다.
        protected override int VisualChildrenCount
        {
            get
            {
                return Child != null ? 1 : 0;
            }
        }
        protected override Visual GetVisualChild(int index)
        {
            if(index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");

            return Child;
        }

        // 이 엘리먼트가 필요로 하는 최소 크기를 측정한다.(sizeDesired)
        // 이때, 하위 엘리먼트의 크기를 측정해서 하위 엘리먼트의 크기 최소치를 고려해준다.(Child.Measure와 Child.DesiredSize)
        // 이와 같은 과정을 반복하면 해당 엘리먼트 하위에 있는 모든 엘리먼트에 대한 크기를 고려할 수 있다.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);
            if(Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;

                sizeAvailable.Width = 
                    Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
                sizeAvailable.Height = 
                    Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);
            }

            if(Child != null)
            {
                Child.Measure(sizeAvailable);

                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height;
            }

            return sizeDesired;
        }

        // 정해진 크기를 넣어 하위 엘리먼트들의 위치를 고려해 실제로 배치한다.
        // MeasureOverride와 마찬가지로 하위 엘리먼트에 대해서도 Arrange를 반복해 모든 엘리먼트를 배치한다.
        // 이 메소드에서 반환하는 값은 그대로 RenderSize에 적용되므로 이를 고려해 반환한다.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            if(Child != null)
            {
                Rect rect = new Rect(
                    new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2,
                    (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                    Child.DesiredSize);
                Child.Arrange(rect);
            }
            return sizeFinal;
        }
    }
}
