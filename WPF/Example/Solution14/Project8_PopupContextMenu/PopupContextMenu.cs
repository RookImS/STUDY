using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Project8_PopupContextMenu
{
    public class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold, itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PopupContextMenu());
        }

        public PopupContextMenu()
        {
            Title = "Popup Context Menu";

            // 컨텍스트 메뉴는 오른쪽 클릭했을 때 나타나는 메뉴창이다.
            menu = new ContextMenu();

            itemBold = new MenuItem();
            itemBold.Header = "Bold";
            menu.Items.Add(itemBold);

            itemItalic = new MenuItem();
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);

            // 모든 TextDecorationLocation 멤버를 구함
            TextDecorationLocation[] locs = (TextDecorationLocation[])Enum.GetValues(typeof(TextDecorationLocation));

            itemDecor = new MenuItem[locs.Length];
            for(int i = 0; i < locs.Length; i++)
            {
                TextDecoration decor = new TextDecoration();
                decor.Location = locs[i];

                itemDecor[i] = new MenuItem();
                itemDecor[i].Header = locs[i].ToString();
                itemDecor[i].Tag = decor;
                menu.Items.Add(itemDecor[i]);
            }

            // 컨텍스트 메뉴에 포함된 각각의 MenuItem 객체의 Click 이벤트에
            // 일일이 핸들러를 추가하지 않기 위해서 상위 객체인 Menu에 임의로 핸들러를 추가해서 해당 핸들러를 처리할 수 있도록 한다.
            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuOnClick));

            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            string strQuote = "To be, or not to be, that is the question";
            string[] strWords = strQuote.Split();

            foreach (string str in strWords)
            {
                Run run = new Run(str);

                run.TextDecorations = new TextDecorationCollection();
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }

        // 마우스 오른쪽 클릭을 했을 때, 적합한 조건이면 ContextMenu를 띄우고, 필요한 작업을 한다.
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonUp(args);

            if((inlClicked = args.Source as Inline) != null)
            {
                itemBold.IsChecked = (inlClicked.FontWeight == FontWeights.Bold);
                itemItalic.IsChecked = (inlClicked.FontStyle == FontStyles.Italic);

                foreach (MenuItem item in itemDecor)
                    item.IsChecked = (inlClicked.TextDecorations.Contains(item.Tag as TextDecoration));

                // ContextMenu를 띄운다.
                menu.IsOpen = true;
                args.Handled = true;
            }
        }

        // 선택된 메뉴항목에 대해 필요한 처리를 한다.
        void MenuOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = args.Source as MenuItem;

            item.IsChecked ^= true;

            if (item == itemBold)
                inlClicked.FontWeight = (item.IsChecked ? FontWeights.Bold : FontWeights.Normal);
            else if(item == itemItalic)
                inlClicked.FontStyle = (item.IsChecked ? FontStyles.Italic : FontStyles.Normal);
            else
            {
                if (item.IsChecked)
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                else
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
            }
        }
    }
}
