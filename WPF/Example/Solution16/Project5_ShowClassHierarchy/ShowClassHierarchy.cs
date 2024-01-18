using System;
using System.Windows;

namespace Project5_ShowClassHierarchy
{
    class ShowClassHierarchy : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShowClassHierarchy());
        }

        public ShowClassHierarchy()
        {
            Title = "Show Class Hierarchy";

            ClassHierarchyTreeView treevue =
                new ClassHierarchyTreeView(
                    typeof(System.Windows.Threading.DispatcherObject));

            Content = treevue;
        }
    }
}
