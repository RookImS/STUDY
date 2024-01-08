using Project10_SelectColorFromGrid;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project3_FormatRichText
{
    public partial class FormatRichText : Window
    {
        ComboBox comboFamily, comboSize;
        ToggleButton btnBold, btnItalic;
        ColorGridBox clrboxBackground, clrboxForeground;

        void AddCharToolBar(ToolBarTray tray, int band, int index)
        {
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            comboFamily = new ComboBox();
            comboFamily.Width = 144;
            comboFamily.ItemsSource = Fonts.SystemFontFamilies;
            comboFamily.SelectedItem = txtbox.FontFamily;
            comboFamily.SelectionChanged += FamilyComboOnSelection;
            toolbar.Items.Add(comboFamily);

            ToolTip tip = new ToolTip();
            tip.Content = "Font Family";
            comboFamily.ToolTip = tip;

            comboSize = new ComboBox();
            comboSize.Width = 48;
            comboSize.IsEditable = true;
            comboSize.Text = (0.75 * txtbox.FontSize).ToString();
            comboSize.ItemsSource = new double[]
            {
                8, 9, 10, 11, 12, 14, 16, 18,
                20, 22, 24, 26, 28, 36, 38, 72
            };
            comboSize.SelectionChanged += SizeComboOnSelection;
            comboSize.GotKeyboardFocus += SizeComboOnGotFocus;
            comboSize.LostKeyboardFocus += SizeComboOnLostFocus;
            comboSize.PreviewKeyDown += SizeComboOnKeyDown;
            toolbar.Items.Add(comboSize);

            tip = new ToolTip();
            tip.Content = "Font Size";
            comboSize.ToolTip = tip;

            btnBold = new ToggleButton();
            btnBold.Checked += BoldButtonOnChecked;
            btnBold.Unchecked += BoldButtonOnChecked;
            toolbar.Items.Add(btnBold);

            Image img = new Image();
            img.Source = new BitmapImage(
                new Uri("pack://application:,,/Images/boldhs.png"));
            img.Stretch = Stretch.None;
            btnBold.Content = img;

            tip = new ToolTip();
            tip.Content = "Bold";
            btnBold.ToolTip = tip;

            btnItalic = new ToggleButton();
            btnItalic.Checked += ItalicButtonOnChecked;
            btnItalic.Unchecked += ItalicButtonOnChecked;
            toolbar.Items.Add(btnItalic);

            img = new Image();
            img.Source = new BitmapImage(
                new Uri("pack://application:,,/Images/ItalicHS.png"));
            img.Stretch = Stretch.None;
            btnItalic.Content = img;

            tip = new ToolTip();
            tip.Content = "Italic";
            btnItalic.ToolTip = tip;

            toolbar.Items.Add(new Separator());

            Menu menu = new Menu();
            toolbar.Items.Add(menu);

            MenuItem item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(
                new Uri("pack://application:,,/Images/ColorHS.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxBackground = new ColorGridBox();
            clrboxBackground.SelectionChanged += BackgroundOnSelectionChanged;
            item.Items.Add(clrboxBackground);

            tip = new ToolTip();
            tip.Content = "Background Color";
            item.ToolTip = tip;

            item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(
                new Uri("pack://application:,,/Images/Color_fontHS.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxForeground = new ColorGridBox();
            clrboxForeground.SelectionChanged += ForegroundOnSelectionChanged;
            item.Items.Add(clrboxForeground);

            tip = new ToolTip();
            tip.Content = "Foreground Color";
            item.ToolTip = tip;

            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
        }

        void TextBoxOnSelectionChanged(object sender, RoutedEventArgs args)
        {
            object obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontFamilyProperty);
            if ((obj is FontFamily))
                comboFamily.SelectedItem = (FontFamily)obj;
            else
                comboFamily.SelectedIndex = -1;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontSizeProperty);
            if(obj is double)
                comboSize.Text = (0.75 * (double)obj).ToString();
            else
                comboSize.SelectedIndex = -1;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontWeightProperty);
            if (obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);
            if(obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);
            if (obj is FontStyle)
                btnItalic.IsChecked = (FontStyle)obj == FontStyles.Italic;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.BackgroundProperty);
            if(obj!= null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.ForegroundProperty);
            if (obj != null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.ForegroundProperty);
            if (obj != null && obj is Brush)
                clrboxForeground.SelectedValue = (Brush)obj;
        }

        void FamilyComboOnSelection(object sender, SelectionChangedEventArgs args)
        {
            ComboBox combo = args.Source as ComboBox;
            FontFamily family = combo.SelectedItem as FontFamily;

            if (family != null)
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontFamilyProperty, family);

            txtbox.Focus();
        }

        string strOriginal;
        void SizeComboOnGotFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            strOriginal = (sender as ComboBox).Text;
        }

        void SizeComboOnLostFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            double size;

            if (Double.TryParse((sender as ComboBox).Text, out size))
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / 0.75);
            else
                (sender as ComboBox).Text = strOriginal;
        }

        void SizeComboOnKeyDown(object sender, KeyEventArgs args)
        {
            if(args.Key == Key.Escape)
            {
                (sender as ComboBox).Text = strOriginal;
                args.Handled = true;
                txtbox.Focus();
            }
            else if(args.Key == Key.Enter)
            {
                args.Handled = true;
                txtbox.Focus();
            }
        }

        void SizeComboOnSelection(object sender, SelectionChangedEventArgs args)
        {
            ComboBox combo = args.Source as ComboBox;

            if(combo.SelectedIndex != -1)
            {
                double size = (double)combo.SelectedValue;

                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, size / 0.75);
                txtbox.Focus();
            }
        }

        void BoldButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = args.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontWeightProperty, 
                (bool)btn.IsChecked ? FontWeights.Bold : FontWeights.Normal);
        }

        void ItalicButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = args.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontStyleProperty,
                (bool)btn.IsChecked ? FontStyles.Italic : FontStyles.Normal);
        }

        void BackgroundOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ColorGridBox clrbox = args.Source as ColorGridBox;
            txtbox.Selection.ApplyPropertyValue(FlowDocument.BackgroundProperty,
                clrbox.SelectedValue);
        }

        void ForegroundOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ColorGridBox clrbox = args.Source as ColorGridBox;
            txtbox.Selection.ApplyPropertyValue(FlowDocument.ForegroundProperty, 
                clrbox.SelectedValue);
        }
    }
}
