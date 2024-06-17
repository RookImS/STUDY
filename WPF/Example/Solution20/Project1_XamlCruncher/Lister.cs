using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project5_ChooseFont
{
    // ListBox와 같이 동작하면서 포커스를 갖지 않는 컨트롤
    class Lister :ContentControl
    {
        ScrollViewer scroll;
        StackPanel stack;
        ArrayList list = new ArrayList();
        int indexSelected = -1;

        public event EventHandler SelectionChanged;

        public Lister()
        {
            Focusable = false;

            Border bord = new Border();
            bord.BorderThickness = new Thickness(1);
            bord.BorderBrush = SystemColors.ActiveBorderBrush;
            bord.Background = SystemColors.WindowBrush;
            Content = bord;

            scroll = new ScrollViewer();
            scroll.Focusable = false;
            scroll.Padding = new Thickness(2, 0, 0, 0);
            bord.Child = scroll;

            stack = new StackPanel();
            scroll.Content = stack;

            AddHandler(TextBlock.MouseLeftButtonDownEvent, new MouseButtonEventHandler(TextBlockOnMouseLeftButtonDown));

            Loaded += OnLoaded;
        }

        // 처음 Lister가 보여질때 선택된 항목을 보여질 수 있도록 뷰를 스크롤
        void OnLoaded(object sender, RoutedEventArgs args)
        {
            ScrollIntoView();
        }

        // Lister의 항목에 관련된 메소드들
        public void Add(object obj)
        {
            list.Add(obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Add(txtblk);
        }
        public void Insert(int index, object obj)
        {
            list.Insert(index, obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Insert(index, txtblk);
        }
        public void Clear()
        {
            SelectedIndex = -1;
            stack.Children.Clear();
            list.Clear();
        }
        public bool Contains(object obj)
        {
            return list.Contains(obj);
        }
        public int Count
        {
            get { return list.Count; }
        }

        // 입력한 문자에 따라 적절한 항목이 선택될 수 있도록 호출되는 메소드
        public void GoToLetter(char ch)
        {
            int offset = SelectedIndex + 1;
            
            for(int i = 0; i < Count; i++)
            {
                int index = (i + offset) % Count;

                if(Char.ToUpper(ch) == Char.ToUpper(list[index].ToString()[0]))
                {
                    SelectedIndex = index;
                    break;
                }
            }
        }

        // 현재 선택된 항목이 적절하게 출력될 수 있도록 돕는 프로퍼티
        public int SelectedIndex
        {
            set
            {
                if (value < -1 || value >= Count)
                    throw new ArgumentOutOfRangeException("SelectedIndex");

                if (value == indexSelected)
                    return;

                if (indexSelected != -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.WindowBrush;
                    txtblk.Foreground = SystemColors.WindowTextBrush;
                }

                indexSelected = value;

                if(indexSelected > -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.HighlightBrush;
                    txtblk.Foreground = SystemColors.HighlightTextBrush;
                }
                ScrollIntoView();

                OnSelectionChanged(EventArgs.Empty);
            }
            get
            {
                return indexSelected;
            }
        }

        // 아이템을 선택할때 사용하는 프로퍼티
        public object SelectedItem
        {
            // 선택된 아이템을 바꾸면 인덱스에 관련된 프로퍼티를 활용해 자동으로 출력을 바꿔준다.
            set
            {
                SelectedIndex = list.IndexOf(value);
            }
            get
            {
                if (SelectedIndex > -1)
                    return list[SelectedIndex];

                return null;
            }
        }

        // 스크롤 뷰를 업다운할 때 사용하는 메소드
        public void PageUp()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex - (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);
            if (index < 0)
                index = 0;

            SelectedIndex = index;
        }
        public void PageDown()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex + (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);
            if(index > Count - 1)
                index = Count - 1;

            SelectedIndex = index;
        }

        // 뷰에서 선택 항목이 보이도록 스크롤해주는 메소드
        void ScrollIntoView()
        {
            if (Count == 0 || SelectedIndex == -1 || scroll.ViewportHeight > scroll.ExtentHeight)
                return;

            double heightPerItem = scroll.ExtentHeight / Count;
            double offsetItemTop = SelectedIndex * heightPerItem;
            double offsetItemBot = (SelectedIndex + 1) * heightPerItem;

            if (offsetItemTop < scroll.VerticalOffset)
                scroll.ScrollToVerticalOffset(offsetItemTop);
            else if (offsetItemBot > scroll.VerticalOffset + scroll.ViewportHeight)
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset + offsetItemBot - scroll.VerticalOffset - scroll.ViewportHeight);
        }

        void TextBlockOnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if(args.Source is TextBlock)
                SelectedIndex = stack.Children.IndexOf(args.Source as TextBlock);
        }

        protected virtual void OnSelectionChanged(EventArgs args)
        {
            if(SelectionChanged != null)
                SelectionChanged(this, args);
        }
    }
}
