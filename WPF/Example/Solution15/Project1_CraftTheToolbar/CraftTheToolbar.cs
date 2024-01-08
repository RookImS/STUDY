using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project1_CraftTheToolbar
{
    public class CraftTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CraftTheToolbar());
        }

        public CraftTheToolbar()
        {
            Title = "Craft the Toolbar";

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New, ApplicationCommands.Open,
                ApplicationCommands.Save, ApplicationCommands.Print,
                ApplicationCommands.Cut, ApplicationCommands.Copy,
                ApplicationCommands.Paste, ApplicationCommands.Delete
            };

            string[] strImages =
            {
                "NewDocumentHS.png", "openHS.png", "saveHS.png",
                "PrintHS.png", "CutHS.png", "CopyHS.png",
                "PasteHS.png", "DeleteHS.png"
            };

            DockPanel dock = new DockPanel();
            // dock.LastChildFill = false;
            Content = dock;

            ToolBar toolbar = new ToolBar();
            dock.Children.Add(toolbar);
            DockPanel.SetDock(toolbar, Dock.Top);

            RichTextBox txtbox = new RichTextBox();
            dock.Children.Add(txtbox);
            txtbox.Focus();

            for(int i = 0; i < 8; i++)
            {
                if (i == 4)
                    toolbar.Items.Add(new Separator());

                // 각 버튼에 위에서 정의한 커맨드들을 설정해준다.
                Button btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(new Uri("pack://application:,,/Images/" + strImages[i]));
                img.Stretch = Stretch.None;

                // 스택패널을 이용해 이미지와 글을 모두 넣을 수 있다.
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                btn.Content = stack;

                TextBlock txtblk = new TextBlock();
                txtblk.Text = comm[i].Text;
                stack.Children.Add(img);
                stack.Children.Add(txtblk);

                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;

                // 툴바의 각 버튼에 대해 표준 커맨드를 바인딩한다.
                // 이때, 위에서 만들어 넣은 RichTextBox가 잘라내기, 복사 등의 커맨드를 이미 바인딩하고 있다.
                // 그렇기 때문에 RichTextBox에서 잘라내기, 복사를 못하는 상황이 되면 해당 객체의 CanExecute핸들러가 작동해서 해당 커맨드를 사용하지 못하게 막는다.
                // 그러므로 자동으로 해당 커맨드를 Command프로퍼티에 설정한 버튼은 비활성화된다.
                // 다만 지금 여기서 구현한 버튼의 Command에 대한 핸들러는 ToolBarButtonOnClick이고, CanExecute핸들러는 설정하지 않았기 때문에
                // 외관적으로 비활성화 돼있더라도, 포커스를 RichTextBox에서 벗어난 윈도우 내부의 어딘가로 두고 해당 단축키를 실행하면 핸들러가 정상적으로 작동하는 것을 볼 수 있다.
                CommandBindings.Add(new CommandBinding(comm[i], ToolBarButtonOnClick));
            }
        }
        void ToolBarButtonOnClick(object sender, ExecutedRoutedEventArgs args)
        {
            RoutedUICommand comm = args.Command as RoutedUICommand;
            MessageBox.Show(comm.Name + " command not yet implemented", Title);
        }
    }
}
