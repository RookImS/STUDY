using Project5_ChooseFont;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Project1_NotepadClone
{
    public partial class NotepadClone
    {
        void AddFormatMenu(Menu menu)
        {
            MenuItem itemFormat = new MenuItem();
            itemFormat.Header = "F_ormat";
            menu.Items.Add(itemFormat);

            WordWrapMenuItem itemWrap = new WordWrapMenuItem();
            itemFormat.Items.Add(itemWrap);

            // 바인딩을 통해서 텍스트박스의 TextWrappingProperty와 새로 만든 메뉴인 WordWrapMenuItem의 WordWrapProperty를 서로 연동한다.
            // TwoWay로 설정해서 서로 반영될 수 있도록 한다.
            Binding bind = new Binding();
            bind.Path = new PropertyPath(TextBox.TextWrappingProperty);
            bind.Source = txtbox;
            bind.Mode = BindingMode.TwoWay;
            itemWrap.SetBinding(WordWrapMenuItem.WordWrapProperty, bind);

            MenuItem itemFont = new MenuItem();
            itemFont.Header = "_Font...";
            itemFont.Click += FontOnClick;
            itemFormat.Items.Add(itemFont);
        }

        void FontOnClick(object sender, RoutedEventArgs args)
        {
            FontDialog dlg = new FontDialog();
            dlg.Owner = this;

            dlg.Typeface = new Typeface(txtbox.FontFamily, txtbox.FontStyle, txtbox.FontWeight, txtbox.FontStretch);
            dlg.FaceSize = txtbox.FontSize;

            if(dlg.ShowDialog().GetValueOrDefault())
            {
                txtbox.FontFamily = dlg.Typeface.FontFamily;
                txtbox.FontSize = dlg.FaceSize;
                txtbox.FontStyle = dlg.Typeface.Style;
                txtbox.FontWeight = dlg.Typeface.Weight;
                txtbox.FontStretch = dlg.Typeface.Stretch;
            }
        }
    }
}
