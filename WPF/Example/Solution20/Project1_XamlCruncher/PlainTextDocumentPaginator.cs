using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Project1_NotepadClone
{
    public class PlainTextDocumentPaginator : DocumentPaginator
    {
        char[] charsBreak = new char[] { ' ', '-' };
        string txt = "";
        string txtHeader = null;
        Typeface face = new Typeface("");
        double em = 11;
        Size sizePage = new Size(8.5 * 96, 11 * 96);
        Size sizeMax = new Size(0, 0);
        Thickness margins = new Thickness(96);
        PrintTicket prntkt = new PrintTicket();
        TextWrapping txtwrap = TextWrapping.Wrap;

        List<DocumentPage> listPages;

        public string Text
        {
            set { txt = value; }
            get { return txt; }
        }
        public TextWrapping TextWrapping
        {
            set { txtwrap = value; }
            get { return txtwrap; }
        }
        public Thickness Margins
        {
            set { margins = value; }
            get { return margins; }
        }
        public Typeface Typeface
        {
            set { face = value; }
            get { return face; }
        }
        public double FaceSize
        {
            set { em = value; }
            get { return em; }
        }
        public PrintTicket PrintTicket
        {
            set { prntkt = value; }
            get { return prntkt; }
        }
        public string Header
        {
            set { txtHeader = value; }
            get { return txtHeader; }
        }

        // 오버라이딩
        public override bool IsPageCountValid
        {
            get
            {
                if (listPages == null)
                    Format();

                return true;
            }
        }
        public override int PageCount
        {
            get
            {
                if (listPages == null)
                    return 0;

                return listPages.Count;
            }
        }
        public override Size PageSize
        {
            set { sizePage = value; }
            get { return sizePage; }
        }
        public override DocumentPage GetPage(int numPage)
        {
            return listPages[numPage];
        }
        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }

        class PrintLine
        {
            public string String;
            public bool Flag;

            public PrintLine(string str, bool flag)
            {
                String = str;
                Flag = flag;
            }
        }

        // 전체 문서를 페이지 단위로 포맷팅
        void Format()
        {
            // 각 줄을 저장
            List<PrintLine> listLines = new List<PrintLine>();

            // 기본적인 계산을 할때 사용
            FormattedText formtxtSample = GetFormattedText("W");

            // 인쇄되는 줄의 폭
            double width = PageSize.Width - Margins.Left - Margins.Right;

            if (width < formtxtSample.Width)
                return;

            string strLine;
            Pen pn = new Pen(Brushes.Black, 2);
            StringReader reader = new StringReader(txt);

            while (null != (strLine = reader.ReadLine()))
                ProcessLine(strLine, width, listLines);
            reader.Close();

            double heightLine = formtxtSample.LineHeight + formtxtSample.Height;
            double height = PageSize.Height - Margins.Top - Margins.Bottom;
            int linesPerPage = (int)(height / heightLine);

            if (linesPerPage < 1)
                return;

            int numPages = (listLines.Count + linesPerPage - 1) / linesPerPage;
            double xStart = Margins.Left;
            double yStart = Margins.Top;

            listPages = new List<DocumentPage>();

            // 위에서 정리한 정보를 바탕으로 페이지 인쇄
            for (int iPage = 0, iLine = 0; iPage < numPages; iPage++)
            {
                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();

                if (Header != null & Header.Length > 0)
                {
                    FormattedText formtxt = GetFormattedText(Header);
                    formtxt.SetFontWeight(FontWeights.Bold);
                    Point ptText = new Point(xStart, yStart - 2 * formtxt.Height);
                    dc.DrawText(formtxt, ptText);
                }

                // 페이지 하단에 꼬리말 출력
                if (numPages > 1)
                {
                    FormattedText formtxt = GetFormattedText("Page " + (iPage + 1) + " of " + numPages);
                    formtxt.SetFontWeight(FontWeights.Bold);
                    Point ptText = new Point((PageSize.Width + Margins.Left - Margins.Right - formtxt.Width) / 2, PageSize.Height - Margins.Bottom + formtxt.Height);
                    dc.DrawText(formtxt, ptText);
                }

                // 페이지 상의 각 줄에 대해 처리
                for (int i = 0; i < linesPerPage; i++, iLine++)
                {
                    if (iLine == listLines.Count)
                        break;

                    string str = listLines[iLine].String;
                    FormattedText formtxt = GetFormattedText(str);
                    Point ptText = new Point(xStart, yStart + i * heightLine);
                    dc.DrawText(formtxt, ptText);

                    // 작은 화살표 플래그 출력
                    if (listLines[iLine].Flag)
                    {
                        double x = xStart + width + 6;
                        double y = yStart + i * heightLine + formtxt.Baseline;
                        double len = face.CapsHeight * em;
                        dc.DrawLine(pn, new Point(x, y), new Point(x + len, y - len));
                        dc.DrawLine(pn, new Point(x, y), new Point(x, y - len / 2));
                        dc.DrawLine(pn, new Point(x, y), new Point(x + len / 2, y));
                    }
                }
                dc.Close();

                DocumentPage page = new DocumentPage(vis);
                listPages.Add(page);
            }
            reader.Close();
        }

        // 텍스트가 여러 줄로 이루어진 텍스트인 경우를 처리
        void ProcessLine(string str, double width, List<PrintLine> list)
        {
            str = str.TrimEnd(' ');

            if(TextWrapping == TextWrapping.NoWrap)
            {
                do
                {
                    int length = str.Length;

                    while (GetFormattedText(str.Substring(0, length)).Width > width)
                        length--;

                    list.Add(new PrintLine(str.Substring(0, length), length < str.Length));
                }
                while (str.Length > 0);
            }
            else
            {
                do
                {
                    int length = str.Length;
                    bool flag = false;

                    while(GetFormattedText(str.Substring(0, length)).Width > width)
                    {
                        int index = str.LastIndexOfAny(charsBreak, length - 2);

                        if (index != -1)
                            length = index + 1;
                        else
                        {
                            // 공백이나 대시가 있는지 검사
                            index = str.IndexOfAny(charsBreak);

                            if(index != -1)
                                length = index + 1;
                            
                            if(TextWrapping == TextWrapping.Wrap)
                            {
                                while (GetFormattedText(str.Substring(0, length)).Width > width)
                                    length--;

                                flag = true;
                            }
                            break;
                        }
                    }
                    list.Add(new PrintLine(str.Substring(0, length), flag));
                    str = str.Substring(length);
                } while (str.Length > 0);
            }
        }

        FormattedText GetFormattedText(string str)
        {
            return new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, em, Brushes.Black);
        }
    }
}
