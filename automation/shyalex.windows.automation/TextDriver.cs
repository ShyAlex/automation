using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class TextDriver : IControlDriver
    {
        private readonly AutomationElement element;

        public AutomationElement Element { get { return element; } }

        public TextDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

        public String GetText()
        {
            var textPattern = (TextPattern)element.GetCurrentPattern(TextPattern.Pattern);
            return textPattern.DocumentRange.GetText(-1);
        }
    }
}
