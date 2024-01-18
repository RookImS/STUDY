﻿using System;
using System.Windows.Controls;

namespace Project5_ShowClassHierarchy
{
    class TypeTreeViewItem : TreeViewItem
    {
        Type typ;

        public TypeTreeViewItem()
        {

        }
        public TypeTreeViewItem(Type typ)
        {
            Type = typ;
        }

        public Type Type
        {
            set
            {
                typ = value;

                if (typ.IsAbstract)
                    Header = typ.Name + " (abstract)";
                else
                    Header = typ.Name;
            }
            get { return typ; }
        }
    }
}
