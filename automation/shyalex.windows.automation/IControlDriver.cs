using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public interface IControlDriver
    {
        AutomationElement Element { get; }
    }
}
