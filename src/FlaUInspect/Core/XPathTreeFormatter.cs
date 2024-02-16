using FlaUI.Core.Conditions;
using FlaUI.Core;
using System;
using FlaUI.Core.AutomationElements;
using FlaUInspect.Core;

namespace FlaUInspect.Core
{
    public static class XPathChildrenFormatter
    {
        public static string GetXPathOfAllChildren(AutomationElement element)
        {
            string xpathtree = "";

            foreach (var child in element.FindAllChildren())
            {
                xpathtree += XPathFormatter.GetXPathToElement(child, null) + "\n";

            }
            return xpathtree;
        }
        

    }
}
