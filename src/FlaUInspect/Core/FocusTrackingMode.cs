using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.EventHandlers;

namespace FlaUInspect.Core
{
    public class FocusTrackingMode
    {
        private readonly AutomationBase _automation;
        private FocusChangedEventHandlerBase _eventHandler;
        private AutomationElement _currentFocusedElement;
        private readonly DispatcherTimer _dispatcherTimer;
        
        public event Action<AutomationElement> ElementFocused;

        public FocusTrackingMode(AutomationBase automation)
        {
            _automation = automation;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
        }

        public void Start()
        {
            _currentFocusedElement = null;
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _currentFocusedElement = null;
            _dispatcherTimer.Stop();
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightAlt))
            {
                OnFocusChanged(_automation.FocusedElement());
            }
        }

        private void OnFocusChanged(AutomationElement automationElement)
        {
            // Skip items in the current process
            // Like Inspect itself or the overlay window
            if (automationElement == null)
            {
                return;
            }
            if (automationElement.Properties.ProcessId == Process.GetCurrentProcess().Id)
            {
                return;
            }
            if (!Equals(_currentFocusedElement, automationElement))
            {
                _currentFocusedElement = automationElement;
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    ElementFocused?.Invoke(automationElement);
                });
            }
        }
    }
}
