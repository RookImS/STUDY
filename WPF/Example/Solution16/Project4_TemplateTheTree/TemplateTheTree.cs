using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Project4_TemplateTheTree
{
    public class TemplateTheTree : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TemplateTheTree());
        }

        public TemplateTheTree()
        {
            Title = "Template the Tree";

            TreeView treevue = new TreeView();
            Content = treevue;

            HierarchicalDataTemplate template = new HierarchicalDataTemplate(typeof(DiskDirectory));

            // 템플릿으로 만들어질 객체는 Subdirectories 프로퍼티가 ItemsSource로 바로 바인딩 된다.
            template.ItemsSource = new Binding("Subdirectories");
            
            // Name이 해당 텍스트블록의 내용과 연동된다.
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            // 텍스트 블록이 실제로 이 템플릿이 보여지게 되는 모양이다.
            template.VisualTree = factoryTextBlock;

            DiskDirectory dir = new DiskDirectory(
                new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory)));

            TreeViewItem item = new TreeViewItem();
            item.Header = dir.Name;
            item.ItemsSource = dir.Subdirectories;
            // 템플릿으로 아이템 객체를 설정한다.
            item.ItemTemplate = template;

            treevue.Items.Add(item);
            item.IsExpanded = true;
        }
    }
}
