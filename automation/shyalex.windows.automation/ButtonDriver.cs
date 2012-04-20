using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class ButtonDriver : IControlDriver
    {
        private readonly AutomationElement element;

        public AutomationElement Element { get { return element; } }

        public ButtonDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

        public void Invoke()
        {
            var invokePattern = (InvokePattern)element.GetCurrentPattern(InvokePattern.Pattern);
            invokePattern.Invoke();
        }
    }
}
