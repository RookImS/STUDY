using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;

namespace Project5_FormatText
{
    class FormatTheText : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatTheText());
        }

        // TextBlock클래스는 FrameworkElement를 상속받았으므로 UIElement라고 할 수 있다.
        // 이때, TextBlock에는 Inlines라는 프로퍼티가 있고 이것은 Inline객체의 collection이다.
        // Inline클래스는 ContentElement를 상속받고 있는데 ContentElement에는 OnRender메소드가 없다.
        // 대신 OnRender를 가지고 있는 UIElement를 통해서 ContentElement객체의 내용을 그려내게 된다.
        // 그러므로 TextBlock객체에는 Inline객체들이 있고, Inline객체들의 내용은 TextBlock객체의 OnRender를 통해 그려진다는 것을 알 수 있다.
        public FormatTheText()
        {
            Title = "Format the Text";

            // Foreground를 설정하면 TextBlock의 텍스트가 지정된 색으로 출력된다.
            // 이는 화면 상에 보이는 각각의 엘리먼트가 부모-자식 계층도 형태의 트리로 존재하기 때문이다.
            // 자식 엘리먼트는 기본적으로 부모 엘리먼트의 모든 프로퍼티 값을 계승한다.
            // 하지만 원한다면 자식 엘리먼트에서 원하는 프로퍼티를 명시적으로 설정할 수도 있다.
            Foreground = Brushes.CornflowerBlue;

            TextBlock txt = new TextBlock();
            txt.FontSize = 32;
            txt.Inlines.Add("This is some ");
            txt.Inlines.Add(new Italic(new Run("italic")));
            txt.Inlines.Add(" text, and this is some ");
            txt.Inlines.Add(new Bold(new Run("bold")));
            txt.Inlines.Add(" text, and let's cap it off with some ");
            txt.Inlines.Add(new Bold(new Italic(new Run("bold italic"))));
            txt.Inlines.Add(" text.");
            txt.TextWrapping = TextWrapping.Wrap;
            Content = txt;
        }
    }
}
