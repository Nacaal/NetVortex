using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Harlinn.Depends4Net.Types
{
    public class NodeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate result = null;
            FrameworkElement element = container as FrameworkElement;

            if (item is AssemblyNode)
            {
                result = element.TryFindResource("AssemblyNodeTemplate") as DataTemplate;
            }
            else if (item is ReferencedAssembliesNode)
            {
                result = element.TryFindResource("ReferencedAssembliesNodeTemplate") as DataTemplate;
            }
            else if (item is AssemblyTypesNode)
            {
                result = element.TryFindResource("AssemblyTypesNodeTemplate") as DataTemplate;
            }
            return result;
        }
    }
}
