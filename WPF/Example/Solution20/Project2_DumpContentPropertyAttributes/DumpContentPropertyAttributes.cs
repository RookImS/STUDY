using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace Project2_DumpContentPropertyAttributes
{
    public class DumpContentPropertyAttributes
    {
        [STAThread]
        public static void Main()
        {
            UIElement dummy1 = new UIElement();
            FrameworkElement dummy2 = new FrameworkElement();

            SortedList<string, string> listClass = new SortedList<string, string>();

            string strFormat = "{0,-35}{1}";

            foreach(AssemblyName asmblyname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                foreach(Type type in Assembly.Load(asmblyname).GetTypes())
                {
                    foreach(object obj in type.GetCustomAttributes(typeof(ContentPropertyAttribute), true))
                    {
                        if (type.IsPublic && obj as ContentPropertyAttribute != null)
                            listClass.Add(type.Name, (obj as ContentPropertyAttribute).Name);
                    }
                }
            }

            Console.WriteLine(strFormat, "Class", "Content Property");
            Console.WriteLine(strFormat, "-----", "----------------");

            foreach(string strClass in listClass.Keys)
            {
                Console.WriteLine(strFormat, strClass, listClass[strClass]);
            }
        }
    }
}
