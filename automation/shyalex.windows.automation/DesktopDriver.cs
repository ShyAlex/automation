using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class DesktopDriver : IControlDriver
    {
        public static readonly DesktopDriver Instance = new DesktopDriver(AutomationElement.RootElement);

        private readonly AutomationElement element;

        public AutomationElement Element { get { return element; } }

        private DesktopDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }
    }
}
