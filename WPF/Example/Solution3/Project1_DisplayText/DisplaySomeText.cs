using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project1_DisplayText
{
    public class DisplaySomeText : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DisplaySomeText());
        }

        // Content는 Window가 상속받고 있는 ContentControl의 프로퍼티이다.
        // object이기 때문에 거의 모든 객체를 받을 수 있지만 1개만 받을 수 있다.
        // 하지만 Window는 항상 트리의 루트이어야 하므로 Window를 받게되면 오류가 발생한다.
        public DisplaySomeText()
        {
            Title = "Display Some Text";
            Content = "Content can be simple text!";
            // Content는 object에 있는 ToString을 기본적인 출력으로 사용한다.
            // Content = EventArgs.Empty;

            // 폰트를 바꿀 수 있다.
            // 폰트는 폰트패밀리를 통해 구성돼있으며 폰트 패밀리는 해당 폰트와 관련된 서체의 모음이다.
            // 타입페이스는 폰트 패밀리의 서체를 이용한 변화를 조합한 것이다.
            // FontFamily = new FontFamily("Comic Sans MS");
            // FontSize = 48;
            // 일반적으로 특정한 타입페이스는 아래와 같은 방식을 통해 설정한다.
            // Italic은 폰트에 설정된 Italic체를 사용하는 것이고, Oblique는 기본 폰트를 옆으로 기울여 둔것이다.
            // FontStyle = FontStyles.Italic;
            // FontStyle = FontStyles.Oblique;
            // FontWeight = FontWeights.Bold;

            // Foreground를 활용하면 텍스트 자체의 색을 변경할 수 있다.
            // Brush brush = new LinearGradientBrush(Colors.Black, Colors.White, new Point(0, 0), new Point(1, 1));
            // Background = brush;
            // Foreground = brush;

            // SizeToContent를 활용해서 Content의 크기에 창크기를 맞출 수 있다.
            // SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
