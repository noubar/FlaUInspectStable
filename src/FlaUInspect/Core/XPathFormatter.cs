using FlaUI.Core.Conditions;
using FlaUI.Core;
using System;
using System.Linq;
using System.Text;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Identifiers;

namespace FlaUInspect.Core
{
    public static class XPathFormatter
    {
        public static string GetXPathToElement(AutomationElement element, AutomationElement rootElement = null)
        {
            ITreeWalker controlViewWalker = element.Automation.TreeWalkerFactory.GetControlViewWalker();
            return GetXPathToElement(element, controlViewWalker, rootElement);
        }
        private static string GetXPathToElement(AutomationElement element, ITreeWalker treeWalker, AutomationElement rootElement = null)
        {
            AutomationElement parent = treeWalker.GetParent(element);
            if (parent == null || (rootElement != null && parent.Equals(rootElement)))
            {
                return string.Empty;
            }

            AutomationElement[] array = parent.FindAllChildren((ConditionFactory cf) => cf.ByControlType(element.Properties.ControlType));
            string text = $"{element.Properties.ControlType.Value}";

            var property = element.Format();

            if (!property.Equals(String.Empty))
            {
                text += property;
            }
            else
            {
                if (array.Length > 1)
                {
                    int num = 1;
                    AutomationElement[] array2 = array;
                    for (int i = 0; i < array2.Length && !array2[i].Equals(element); i++)
                    {
                        num++;
                    }
                    text += $"[{num}]";
                }
            }
            return GetXPathToElement(parent, treeWalker, rootElement) + "/" + text;
           
        }

    }
}
