using System;
using System.Windows;

namespace Project7_RenderGraphic
{
    class RenderTheGraphic : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RenderTheGraphic());
        }

        public RenderTheGraphic()
        {
            Title = "Render the Graphic";
            SimpleEllipse elips = new SimpleEllipse();
            Content = elips;
        }
    }
}
