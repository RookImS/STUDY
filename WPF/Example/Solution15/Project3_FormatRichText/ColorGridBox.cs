using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project10_SelectColorFromGrid
{
    class ColorGridBox : ListBox
    {
        string[] strColors =
        {
            "Black", "Brown", "DarkGreen", "MidnightBlue",
            "Navy", "DarkBlue", "Indigo", "DimGray",
            "DarkRed", "OrangeRed", "Olive", "Green",
            "Teal", "Blue", "SlateGray", "Gray",
            "Red", "Orange", "YellowGreen", "SeaGreen",
            "Aqua", "LightBlue", "Violet", "DarkGray",
            "Pink", "Gold", "Yellow", "Lime",
            "Turquoise", "SkyBlue", "Plum", "LightGray",
            "LightPink", "Tan", "LightYellow", "LightGreen",
            "LightCyan", "LightSkyBlue", "Lavender", "White"
        };

        public ColorGridBox()
        {
            // ItemsPanel을 설정함으로써 리스트박스 내부의 항목 나열방식을 바꾼다.
            // 이때 이를 설정하기 위해 ItemsPanelTemplate 인스턴스를 만든다.
            FrameworkElementFactory factoryUnigrid =
                new FrameworkElementFactory(typeof(UniformGrid));
            factoryUnigrid.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanel = new ItemsPanelTemplate(factoryUnigrid);

            foreach (string strColor in strColors)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 12;
                rect.Height = 12;
                rect.Margin = new Thickness(4);
                rect.Fill = (Brush)typeof(Brushes).GetProperty(strColor).GetValue(null, null);

                Items.Add(rect);

                ToolTip tip = new ToolTip();
                tip.Content = strColor;
                rect.ToolTip = tip;
            }

            SelectedValuePath = "Fill";
        }
    }
}
