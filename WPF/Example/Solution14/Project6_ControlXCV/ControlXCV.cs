using System;
using System.Windows;
using System.Windows.Input;

namespace Project6_ControlXCV
{
    public class ControlXCV : Project5_CutCopyAndPaste.CutCopyAndPaste
    {
        // KeyGesture 객체는 특정한 키입력을 저장한다.
        KeyGesture gestCut = new KeyGesture(Key.X, ModifierKeys.Control);
        KeyGesture gestCopy = new KeyGesture(Key.C, ModifierKeys.Control);
        KeyGesture gestPaste = new KeyGesture(Key.V, ModifierKeys.Control);
        KeyGesture gestDelete = new KeyGesture(Key.Delete);

        [STAThread]
        public new static void Main()
        {
            Application app = new Application();
            app.Run(new ControlXCV());
        }

        public ControlXCV()
        {
            Title = "Control X, C, and V";

            // InputGetsutreText 프로퍼티를 활용해서 메뉴 항목의 단축키에 대해서 안내할 수 있다.
            itemCut.InputGestureText = "Ctrl+X";
            itemCopy.InputGestureText = "Ctrl+C";
            itemPaste.InputGestureText = "Ctrl+V";
            itemDelete.InputGestureText = "Delete";
        }

        // 단축키에 해당하는 경우 그 단축키에 해당하는 동작을 실행한다.
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);
            args.Handled = true;

            // 상황에 따라 각 메뉴가 비활성화되어 실행할 수 없는 상황이 있을 수 있다.
            // 하지만 이는 해당 메뉴의 작동 메소드 내에서 해당 상황을 처리하기 때문에 여기서는 일단 실행한다.
            if (gestCut.Matches(null, args))
                CutOnClick(this, args);
            else if (gestCopy.Matches(null, args))
                CopyOnClick(this, args);
            else if (gestPaste.Matches(null, args))
                PasteOnClick(this, args);
            else if (gestDelete.Matches(null, args))
                DeleteOnClick(this, args);
            else
                args.Handled = false;
        }
    }
}
