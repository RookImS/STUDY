using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1_GradientBrushResourceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // 코드 비하인드 C#파일에서도 xaml파일의 리소스를 추가할 수 있다.
            Resources.Add("thicknessMargin", new Thickness(24, 12, 24, 23));
            InitializeComponent();
        }
    }
}