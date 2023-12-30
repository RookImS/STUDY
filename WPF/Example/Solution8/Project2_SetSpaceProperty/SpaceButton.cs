using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_SpaceButton
{
    public class SpaceButton : Button
    {
        // 일반적인 프로퍼티
        string txt;
        public string Text
        {
            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }
            get
            {
                return txt;
            }
        }

        // DependencyProperty를 활용한 프로퍼티
        // 일반적으로 사용하는 프로퍼티처럼 사용을 하면 자연스럽게 DependencyProperty로 등록한 SpaceProperty를 사용할 수 있도록 한다.
        // SetValue, GetValue는 Button이 DependencyObject에서 상속받은 메소드이다.
        public static readonly DependencyProperty SpaceProperty;
        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get
            {
                return (int)GetValue(SpaceProperty);
            }
        }
        
        // 생성자에서 해당 클래스의 DependencyProperty에 대한 정의를 메타데이터를 통해 하고, 이를 실제로 등록까지 마친다.
        static SpaceButton()
        {
            // metadata 정의
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            // 아래 코드덕에 프로퍼티를 통해 값을 바꾸면 이에 맞게 각종 출력이 자동으로 바뀐다.
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // DependencyProperty에 메타데이터 등록
            SpaceProperty = DependencyProperty.Register("Space", typeof(int), typeof(SpaceButton), metadata, ValidateSpaceValue);
        }

        // 등록 시에 의존 프로퍼티의 검증 방법도 함께 등록한다.
        // 의존 프로퍼티와 관련된 메소드이기 때문에 의존 프로퍼티와 마찬가지로 static을 이용한다.
        static bool ValidateSpaceValue(object obj)
        {
            int i = (int)obj;
            return i >= 0;
        }

        static void OnSpacePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        string SpaceOutText(string str)
        {
            if (str == null)
                return null;

            StringBuilder build = new StringBuilder();

            foreach (char ch in str)
                build.Append(ch + new string(' ', Space));

            return build.ToString();
        }
    }

        
}
