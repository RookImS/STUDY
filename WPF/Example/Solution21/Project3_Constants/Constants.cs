using System;
using System.Windows;
using System.Windows.Media;

namespace Project3_Constants
{
    public static class Constants
    {
        public static readonly FontFamily fntfam =
            new FontFamily("Times New Roman Italic");

        public static double FontSize
        {
            get { return 72 / 0.75; }
        }

        public static readonly LinearGradientBrush brush =
            new LinearGradientBrush(Colors.LightGray, Colors.DarkGray, new Point(0, 0), new Point(1, 1));
    }
}
