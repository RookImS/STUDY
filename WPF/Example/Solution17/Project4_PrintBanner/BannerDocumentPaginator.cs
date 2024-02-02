using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Project4_PrintBanner
{
    // DocumentPaginator는 문서를 페이지로 만드는 내용을 정의한 추상클래스이다.
    public class BannerDocumentPaginator : DocumentPaginator
    {
        string txt = "";
        Typeface face = new Typeface("");
        Size sizePage;
        Size sizeMax = new Size(0, 0);

        // BannerDocumentPaginator만을 위한 프로퍼티
        public string Text
        {
            set { txt = value; }
            get { return txt; }
        }
        public Typeface Typeface
        {
            set { face = value; }
            get { return face; }
        }

        FormattedText GetFormattedText(char ch, Typeface face, double em)
        {
            return new FormattedText(ch.ToString(), CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, face, em, Brushes.Black);
        }

        // 아래부터는 DocumentPaginator에 의해 오버라이딩이 필요한 내용들
        public override bool IsPageCountValid
        {
            get
            {
                foreach (char ch in txt)
                {
                    FormattedText formtxt = GetFormattedText(ch, face, 100);
                    sizeMax.Width = Math.Max(sizeMax.Width, formtxt.Width);
                    sizeMax.Height = Math.Max(sizeMax.Height, formtxt.Height);
                }
                return true;
            }
        }

        public override int PageCount
        {
            get{ return txt == null? 0 : txt.Length;}
        }

        public override Size PageSize
        {
            set { sizePage = value; }
            get { return sizePage; }
        }

        // 특정한 페이지를 받으면 그 페이지에 있어야할 내용을 계산해서 DocumentPage객체로 만들어 반환한다.
        public override DocumentPage GetPage(int numPage)
        {
            DrawingVisual vis = new DrawingVisual();
            DrawingContext dc = vis.RenderOpen();

            double factor = Math.Min((PageSize.Width - 96) / sizeMax.Width, (PageSize.Height - 96) / sizeMax.Height);

            FormattedText formtxt = GetFormattedText(txt[numPage], face, factor * 100);

            Point ptText = new Point((PageSize.Width - formtxt.Width) / 2, (PageSize.Height - formtxt.Height) / 2);

            dc.DrawText(formtxt, ptText);
            dc.Close();

            return new DocumentPage(vis);
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }
    }
}
