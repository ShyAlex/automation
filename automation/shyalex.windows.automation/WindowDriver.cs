using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class WindowDriver : IControlDriver
    {
        private readonly AutomationElement element;

        public AutomationElement Element { get { return element; } }

        public WindowDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

        public static WindowDriver GetWindow(String windowAutomationId, Int32 timeoutMs)
        {
            var windowElement = TryGetWindowElement(windowAutomationId, timeoutMs);

            if (windowElement == null)
            {
                throw new TimeoutException(
                    "Could not load application and find " + windowAutomationId + " within " + timeoutMs + "ms");
            }

            return new WindowDriver(windowElement);
        }

        private static AutomationElement TryGetWindowElement(String windowAutomationId, Int32 timeoutMs)
        {
            var desktop = AutomationElement.RootElement;
            var condition = new PropertyCondition(AutomationElement.AutomationIdProperty, windowAutomationId);
            var getWindowElementOperation = new UiOperation<AutomationElement>(
                null,
                () => desktop.FindFirst(TreeScope.Children, condition),
                e => e != null,
                timeoutMs,
                150);
            return getWindowElementOperation.Invoke();
        }

        public void CloseWindow()
        {
            var windowPattern = (WindowPattern)element.GetCurrentPattern(WindowPattern.Pattern);
            windowPattern.Close();
        }
    }
}
