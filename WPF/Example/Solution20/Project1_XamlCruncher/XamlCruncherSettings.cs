using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project1_XamlCruncher
{
    public class XamlCruncherSettings : Project1_NotepadClone.NotepadCloneSettings
    {
        public Dock Orientation = Dock.Left;
        public int TabSpaces = 4;

        public string StartupDocument =
            "<Button xmlns=\"http://schemas.microsoft.com/winfx" +
                        "/2006/xaml/presentation\"\r\n" +
            "       xmlns:x=\"http://schemas.microsoft.com/winfx" +
                        "/2006/xaml\">\r\n" +
            "   Hello, XAML!\r\n" +
            "</Button>\n";

        public XamlCruncherSettings()
        {
            FontFamily = "Lucida Console";
            FontSize = 10 / 0.75;
        }
    }
}
