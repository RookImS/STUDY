using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_FlipBrush
{
    public class FlipThroughTheBrushes : Window
    {
        int index = 0;
        PropertyInfo[] props;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FlipThroughTheBrushes());
        }

        public FlipThroughTheBrushes()
        {
            // 리플렉션을 이용해 브러쉬의 속성 가져옴
            props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
            SetTitleAndBackground();

            // SystemColors에서 얻은 브러시 객체 또한 고정상태이므로 아래와 같이 사용해야 한다.
            Background = new SolidColorBrush(SystemColors.WindowColor);
            
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            if(args.Key == Key.Down || args.Key == Key.Up)
            {
                index += args.Key == Key.Up ? 1 : props.Length - 1;
                index %= props.Length;
                SetTitleAndBackground();
            }
            base.OnKeyDown(args);
        }


        void SetTitleAndBackground()
        {
            Title = "Flip Through the Brushes - " + props[index].Name;
            // 특정한 브러쉬 이름에 대한 객체를 가져온다.
            Background = (Brush)props[index].GetValue(null, null);
        }
    }
}
