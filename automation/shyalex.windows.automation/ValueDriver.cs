using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class ValueDriver : IControlDriver
    {
        private readonly AutomationElement element;

        public AutomationElement Element
        {
            get { return element; }
        }

        public ValueDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

        public void SetValue(String value)
        {
            var valuePattern = (ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern);
            valuePattern.SetValue(value);
        }
    }
}
