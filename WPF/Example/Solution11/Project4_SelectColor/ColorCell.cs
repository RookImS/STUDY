using System;
using System.Windows;
using System.Windows.Media;

namespace Project4_SelectColor
{
    class ColorCell : FrameworkElement
    {
        static readonly Size sizeCell = new Size(20, 20);
        DrawingVisual visColor;
        Brush brush;

        // 의존 프로퍼티를 정의한다. 이때, 각각은 OnRender에 영향을 주므로 AffectesRender를 옵션으로 선택해 등록한다.
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightedProperty;

        static ColorCell()
        {
            IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(ColorCell),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

            IsHighlightedProperty = DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(ColorCell),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public bool IsSelected
        {
            set { SetValue(IsSelectedProperty, value); }
            get {  return (bool)GetValue(IsSelectedProperty); }
        }

        public bool IsHighlighted
        {
            set { SetValue(IsHighlightedProperty, value); }
            get { return (bool)GetValue(IsHighlightedProperty); }
        }

        public Brush Brush
        {
            get { return brush; }
        }

        // visColor는 ColorCell에 속했지만 별개로 먼저 렌더링을 해서 만들어준다.
        // 이를 위해 dc를 정의하고 DrawingVisual을 이용해 이를 그려낸다.
        // 이때, 이 객체 또한 ColorCell로 이벤트를 라우팅해줘야하므로(ColorCell을 선택한 상황을 가정)
        // VisualTree의 Child로 등록해야 한다.
        public ColorCell(Color clr)
        {
            visColor = new DrawingVisual();
            DrawingContext dc = visColor.RenderOpen();

            Rect rect = new Rect(new Point(0, 0), sizeCell);
            rect.Inflate(-4, -4);
            Pen pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();

            AddVisualChild(visColor);
            AddLogicalChild(visColor);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return visColor;
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            return sizeCell;
        }

        // 옵션에서 AffectsRender가 설정돼있기 때문에 의존 프로퍼티들이 바뀔때마다 사용된다.
        protected override void OnRender(DrawingContext dc)
        {
            Rect rect = new Rect(new Point(0, 0), RenderSize);
            rect.Inflate(-1, -1);
            Pen pen = new Pen(SystemColors.HighlightBrush, 1);

            if (IsHighlighted)
                dc.DrawRectangle(SystemColors.ControlDarkBrush, pen, rect);
            else if (IsSelected)
                dc.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            else
                dc.DrawRectangle(Brushes.Transparent, null, rect);
        }
    }
}
