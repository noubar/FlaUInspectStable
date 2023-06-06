using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using FlaUI.Core.AutomationElements;

namespace FlaUInspect.Core
{
    public static class AutomationElementExtensions
    {
        public static string Format(this AutomationElement element)
        { 
            return !String.IsNullOrEmpty(element.Properties.AutomationId.ValueOrDefault) ? $"[@AutomationId='{element.Properties.AutomationId}']" :
                   (!String.IsNullOrEmpty(element.Properties.Name.ValueOrDefault) ? $"[@Name='{element.Properties.Name}']" :
                   (!String.IsNullOrEmpty(element.Properties.ClassName.ValueOrDefault) ? $"[@ClassName='{element.Properties.ClassName}']" : String.Empty));

        }
    }
}
