using System;
using System.Drawing;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;

namespace FlaUInspect.Core
{
    public static class ElementHighlighter
    {
        public static void HighlightElement(AutomationElement automationElement)
        {
            
                Task.Run(() =>
                {
                    try
                    {
                        automationElement.DrawHighlight(false, Color.Red, TimeSpan.FromSeconds(1));
                }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Caught '{ex.GetType()}': {ex.Message}");
                    }
                });
            
        }
    }
}
