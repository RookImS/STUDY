using Project2_SpaceButton;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project2_SetSpaceProperty
{
    public class SpaceWindow : Window
    {
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

        static SpaceWindow()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();

            metadata.Inherits = true;

            // SpaceButton에서 이미 Space에 관련된 의존 프로퍼티를 등록했으므로 해당 프로퍼티의 소유자만을 추가한다.
            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(SpaceWindow));
            // 의존 프로퍼티의 메타데이터를 각 소유자가 공유하는 것은 아니므로
            // 이 클래스에서 의존프로퍼티를 활용하는 방법에 대한 새 메타데이터를 등록한다.
            SpaceProperty.OverrideMetadata (typeof(SpaceWindow), metadata);
        }
    }
}
