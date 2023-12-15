using System;
using System.Windows;
using System.Windows.Media;

namespace Project7_RenderGraphic
{
    // FrameworkElement를 직접 상속받는 클래스를 만들어 특정한 렌더링을 커스텀할 수 있다.
    class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext dc)
        {
            // DrawEllipse는 우리가 가진 프로퍼티를 활용해서 원을 그릴 때 필요한 내용들을 dc에 보관한다.
            // 보관한 내용들을 그대로 활용할 수도 있지만 이후에 화면 상에서 다양한 조합을 할 때 사용되며, wpf는 이를 모두 조합한 결과를 그려서 보여주게 된다.
            // 즉, 이 메소드를 부른 시점에 바로 그림이 그려지는 것이 아니다.
            dc.DrawEllipse(Brushes.Blue, 
                new Pen(Brushes.Red, 24), new Point(RenderSize.Width / 2, RenderSize.Height / 2), 
                RenderSize.Width / 2, RenderSize.Height / 2);
        }
    }
}
