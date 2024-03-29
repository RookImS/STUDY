﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project5_ChooseFont
{
    // Typeface를 설정해주는 대화상자
    public class FontDialog : Window
    {
        TextBoxWithLister boxFamily, boxStyle, boxWeight, boxStretch, boxSize;
        Label lblDisplay;
        bool isUpdateSuppressed = true;

        public Typeface Typeface
        {
            set
            {
                if (boxFamily.Contains(value.FontFamily))
                    boxFamily.SelectedItem = value.FontFamily;
                else
                    boxFamily.SelectedIndex = 0;

                if(boxStyle.Contains(value.Style))
                    boxStyle.SelectedItem = value.Style;
                else
                    boxStyle.SelectedIndex = 0;

                if(boxWeight.Contains(value.Weight))
                    boxWeight.SelectedItem = value.Weight;
                else
                    boxWeight.SelectedIndex = 0;

                if(boxStretch.Contains(value.Stretch))
                    boxStretch.SelectedItem = value.Stretch;
                else
                    boxStretch.SelectedIndex = 0;
            }
            get
            {
                return new Typeface((FontFamily)boxFamily.SelectedItem,
                    (FontStyle)boxStyle.SelectedItem,
                    (FontWeight)boxWeight.SelectedItem,
                    (FontStretch)boxStretch.SelectedItem);
            }
        }
        public double FaceSize
        {
            set
            {
                double size = 0.75 * value;
                boxSize.Text = size.ToString();

                if (!boxSize.Contains(size))
                    boxSize.Insert(0, size);

                boxSize.SelectedItem = size;
            }

            get
            {
                double size;

                if (!Double.TryParse(boxSize.Text, out size))
                    size = 8.25;

                return size / 0.75;
            }
        }

        // 텍스트박스 lister를 이용해 폰트의 각종 설정을 할 수 있도록 한다.
        // 그리고 미리보기를 통해 그 결과를 미리 확인할 수 있다.
        public FontDialog()
        {
            Title = "Font";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            Grid gridMain = new Grid();
            Content = gridMain;

            RowDefinition  rowdef = new RowDefinition();
            rowdef.Height = new GridLength(200, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(150, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridMain.RowDefinitions.Add(rowdef);

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(650, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(coldef);

            Grid gridBoxes = new Grid();
            gridMain.Children.Add(gridBoxes);

            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridBoxes.RowDefinitions.Add(rowdef);

            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            gridBoxes.RowDefinitions.Add(rowdef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(175, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(75, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            Label lbl = new Label();
            lbl.Content = "Font Family";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            boxFamily = new TextBoxWithLister();
            boxFamily.IsReadOnly = true;
            boxFamily.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxFamily);
            Grid.SetRow(boxFamily, 1);
            Grid.SetColumn(boxFamily, 0);

            lbl = new Label();
            lbl.Content = "Style";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 1);

            boxStyle = new TextBoxWithLister();
            boxStyle.IsReadOnly = true;
            boxStyle.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStyle);
            Grid.SetRow(boxStyle, 1);
            Grid.SetColumn(boxStyle, 1);

            lbl = new Label();
            lbl.Content = "Weight";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 2);

            boxWeight = new TextBoxWithLister();
            boxWeight.IsReadOnly = true;
            boxWeight.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxWeight);
            Grid.SetRow(boxWeight, 1);
            Grid.SetColumn(boxWeight, 2);

            lbl = new Label();
            lbl.Content = "Stretch";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 3);

            boxStretch = new TextBoxWithLister();
            boxStretch.IsReadOnly = true;
            boxStretch.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStretch);
            Grid.SetRow(boxStretch, 1);
            Grid.SetColumn(boxStretch, 3);

            lbl = new Label();
            lbl.Content = "Size";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 4);

            boxSize = new TextBoxWithLister();
            boxSize.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxSize);
            Grid.SetRow(boxSize, 1);
            Grid.SetColumn(boxSize, 4);

            // 폰트 설정의 적용결과를 확인할 때 사용
            lblDisplay = new Label();
            lblDisplay.Content = "AaBbCc XxYyZz 012345";
            lblDisplay.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblDisplay.VerticalContentAlignment = VerticalAlignment.Center;
            gridMain.Children.Add(lblDisplay);
            Grid.SetRow(lblDisplay, 1);

            Grid gridButtons = new Grid();
            gridMain.Children.Add(gridButtons);
            Grid.SetRow(gridButtons, 2);

            for (int i = 0; i < 5; i++)
                gridButtons.ColumnDefinitions.Add(new ColumnDefinition());

            Button btn = new Button();
            btn.Content = "OK";
            btn.IsDefault = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            btn.Click += OkOnClick;
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 1);

            btn = new Button();
            btn.Content = "Cancel";
            btn.IsCancel = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 3);

            foreach (FontFamily fam in Fonts.SystemFontFamilies)
                boxFamily.Add(fam);

            double[] ptsizes = new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach(double ptsize in ptsizes)
                boxSize.Add(ptsize);

            // 각 lister에 대해 적절한 핸들러를 설정한다.
            boxFamily.SelectionChanged += FamilyOnSelectionChanged;
            boxStyle.SelectionChanged += StyleOnSelectionChanged;
            boxWeight.SelectionChanged += StyleOnSelectionChanged;
            boxStretch.SelectionChanged += StyleOnSelectionChanged;
            boxSize.TextChanged += SizeOnTextChanged;

            // 프로퍼티가 설정되기 전 현재 설정된 기본값을 설정해준다.
            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            FaceSize = FontSize;

            boxFamily.Focus();

            isUpdateSuppressed = false;
            UpdateSample();
        }

        // 폰트가 바뀌었을 때에 대한 처리를 한다.
        void FamilyOnSelectionChanged(object sender, EventArgs args)
        {
            FontFamily fntfam = (FontFamily)boxFamily.SelectedItem;

            // 이전 설정값을 저장한다.
            FontStyle? fntstyPrevious = (FontStyle?)boxStyle.SelectedItem;
            FontWeight? fntwtPrevious = (FontWeight?)boxWeight.SelectedItem;
            FontStretch? fntstrPrevious = (FontStretch?)boxStretch.SelectedItem;

            isUpdateSuppressed = true;

            // 이 폰트가 가진 스타일에 맞춰 lister를 다시 설정한다.
            boxStyle.Clear();
            boxWeight.Clear();
            boxStretch.Clear();

            foreach(FamilyTypeface ftf in fntfam.FamilyTypefaces)
            {
                if(!boxStyle.Contains(ftf.Style))
                {
                    if (ftf.Style == FontStyles.Normal)
                        boxStyle.Insert(0, ftf.Style);
                    else
                        boxStyle.Add(ftf.Style);
                }

                if(!boxWeight.Contains(ftf.Weight))
                {
                    if(ftf.Weight == FontWeights.Normal)
                        boxWeight.Insert(0, ftf.Weight);
                    else
                        boxWeight.Add(ftf.Weight);
                }

                if(!boxStretch.Contains(ftf.Stretch))
                {
                    if (ftf.Stretch == FontStretches.Normal)
                        boxStretch.Insert(0, ftf.Stretch);
                    else
                        boxStretch.Add(ftf.Stretch);
                }
            }

            if (boxStyle.Contains(fntstyPrevious))
                boxStyle.SelectedItem = fntstyPrevious;
            else
                boxStyle.SelectedIndex = 0;

            if(boxWeight.Contains(fntwtPrevious))
                boxWeight.SelectedItem = fntwtPrevious;
            else
                boxWeight.SelectedIndex = 0;

            if(boxStretch.Contains(fntstrPrevious))
                boxStretch.SelectedItem = fntstrPrevious;
            else
                boxStretch.SelectedIndex = 0;

            // 샘플을 업데이트한다.
            isUpdateSuppressed = false;
            UpdateSample();
        }

        void StyleOnSelectionChanged(object sender, EventArgs args)
        {
            UpdateSample();
        }

        void SizeOnTextChanged(object sender, TextChangedEventArgs args)
        {
            UpdateSample();
        }

        // 현재 설정에 맞게 샘플 텍스트를 갱신한다.
        void UpdateSample()
        {
            if (isUpdateSuppressed)
                return;

            lblDisplay.FontFamily = (FontFamily)boxFamily.SelectedItem;
            lblDisplay.FontStyle = (FontStyle)boxStyle.SelectedItem;
            lblDisplay.FontWeight = (FontWeight)boxWeight.SelectedItem;
            lblDisplay.FontStretch = (FontStretch)boxStretch.SelectedItem;

            double size;

            if (!Double.TryParse(boxSize.Text, out size))
                size = 8.25;

            lblDisplay.FontSize = size / 0.75;
        }

        void OkOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}
