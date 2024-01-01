using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project3_BuildButtonFactory
{
    public class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build Button Factory";

            // Button객체를 위한 ControlTemplate 생성
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // Border클래스에 대한 Factory 생성 및 기본값 설정
            FrameworkElementFactory factoryBorder =
                new FrameworkElementFactory(typeof(Border));
            factoryBorder.Name = "border";
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            // ContentPresenter클래스에 대한 Factory 생성 및 기본값 설정
            FrameworkElementFactory factoryContent =
                new FrameworkElementFactory(typeof(ContentPresenter));
            factoryContent.Name = "content";
            factoryContent.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(Button.ContentProperty));
            factoryContent.SetValue(ContentPresenter.MarginProperty,
                new TemplateBindingExtension(Button.PaddingProperty));

            // 이미 만든 factory간의 Child관계를 지정
            factoryBorder.AppendChild(factoryContent);

            // 이 템플릿의 VisualTree의 루트가 될 엘리먼트를 설정
            template.VisualTree = factoryBorder;

            // 의존 프로퍼티에 대한 트리거를 만들어 의존 프로퍼티 변화에 대해서 반응할 수 있는 체계를 만듦
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            // 특정 프로퍼티에 대한 변화 내용을 설정 후 원하는 트리거에 넣어 의존 프로퍼티에 의해 해당 변화가 일어날 수 있도록 만듦
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";
            trig.Setters.Add(set);

            set= new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;
            trig.Setters.Add(set);

            // 템플릿에 트리거에 대한 정보를 넣어 템플릿에서 트리거와 관련된 의존 프로퍼티가 변화할 때 특정 행동을 할 수 있게 해줌
            template.Triggers.Add(trig);

            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;

            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";
            trig.Setters.Add(set);

            template.Triggers.Add(trig);

            // 버튼을 생성해 이미 만든 템플릿을 불러온 후, 세세한 설정을 해서 버튼을 완성
            Button btn = new Button();
            btn.Template = template;

            btn.Content = "Button with Custom Template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("you clicked the button", Title);
        }
    }
}
