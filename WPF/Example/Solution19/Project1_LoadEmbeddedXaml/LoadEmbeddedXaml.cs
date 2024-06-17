using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Project1_LoadEmbeddedXaml
{
    public class LoadEmbeddedXaml : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LoadEmbeddedXaml());
        }

        public LoadEmbeddedXaml()
        {
            Title = "Load Embedded Xaml";

            string strXaml =
                "<Button xmlns='http://schemas.microsoft.com/" +
                                "winfx/2006/xaml/presentation'" +
                "       Foreground='LightSeaGreen' FontSize='24pt'>" +
                "   Click me!" +
                "</Button>";

            StringReader strreader = new StringReader(strXaml);
            XmlTextReader xmlreader = new XmlTextReader(strreader);
            object obj = XamlReader.Load(xmlreader);

            // 버튼으로 인식할 수도 있으며 이를 활용해 이벤트 핸들러를 연결할 수도 있다.
            // Button btn = (Button) XamlReader.Load(xmlreader);
            // btn.Click += ButtonOnClick;

            Content = obj;
        }
    }
}
