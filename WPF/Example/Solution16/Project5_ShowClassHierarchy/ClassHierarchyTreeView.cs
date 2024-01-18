using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Project5_ShowClassHierarchy
{
    public class ClassHierarchyTreeView : TreeView
    {
        public ClassHierarchyTreeView(Type typeRoot)
        {
            UIElement dummy = new UIElement();

            List<Assembly> assemblies = new List<Assembly>();

            // 참조하는 어셈블리를 모두 구해 어셈블리 목록에 추가
            AssemblyName[] anames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach(AssemblyName aname in anames)
                assemblies.Add(Assembly.Load(aname));

            // sorted리스트에 typeRoot의 하위 요소 저장
            SortedList<string, Type> classes = new SortedList<string, Type>();
            classes.Add(typeRoot.Name, typeRoot);

            // 어셈블리 내의 모든 타입을 구함
            foreach (Assembly assembly in assemblies)
                foreach (Type typ in assembly.GetTypes())
                    if (typ.IsPublic && typ.IsSubclassOf(typeRoot))
                        classes.Add(typ.Name, typ);

            // 루트 항목 생성
            TypeTreeViewItem item = new TypeTreeViewItem(typeRoot);
            Items.Add(item);

            CreateLinkedItems(item, classes);
        }

        void CreateLinkedItems(TypeTreeViewItem itemBase, SortedList<string, Type> list)
        {
            foreach(KeyValuePair<string, Type> kvp in list)
                if(kvp.Value.BaseType == itemBase.Type)
                {
                    TypeTreeViewItem item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
        }
    }
}
