using System;
using System.Windows;
using System.Windows.Controls;

namespace Project1_NotepadClone
{
    public class WordWrapMenuItem :MenuItem
    {
        public static DependencyProperty WordWrapProperty = 
            DependencyProperty.Register("WordWrap", typeof(TextWrapping), typeof(WordWrapMenuItem));

        public TextWrapping WordWrap
        {
            set { SetValue(WordWrapProperty, value); }
            get { return (TextWrapping)GetValue(WordWrapProperty); }
        }

        public WordWrapMenuItem()
        {
            Header = "_Word Wrap";

            MenuItem item = new MenuItem();
            item.Header = "_No Wrap";
            item.Tag = TextWrapping.NoWrap;
            item.Click += MenuItemOnClick;
            Items.Add(item);

            item = new MenuItem();
            item.Header = "_Wrap";
            item.Tag = TextWrapping.Wrap;
            item.Click += MenuItemOnClick;
            Items.Add(item);

            item = new MenuItem();
            item.Header = "Wrap with _Overflow";
            item.Tag = TextWrapping.WrapWithOverflow;
            item.Click += MenuItemOnClick;
            Items.Add(item);
        }

        protected override void OnSubmenuOpened(RoutedEventArgs args)
        {
            base.OnSubmenuOpened(args);

            foreach (MenuItem item in Items)
                item.IsChecked = ((TextWrapping)item.Tag == WordWrap);
        }

        void MenuItemOnClick (object sender, RoutedEventArgs args) 
        {
            WordWrap = (TextWrapping)(args.Source as MenuItem).Tag;
        }
    }
}
