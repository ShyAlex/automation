using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public static class IControlDriverExtensions
    {
        public static T GetChildDriverByName<T>(this IControlDriver parent, String name, Func<AutomationElement, T> toDriver) where T : IControlDriver
        {
            if (toDriver == null)
            {
                throw new ArgumentNullException("toDriver");
            }

            return toDriver(parent.GetChildElementByName(name));
        }

        public static T GetChildDriverById<T>(this IControlDriver parent, String automationId, Func<AutomationElement, T> toDriver) where T : IControlDriver
        {
            if (toDriver == null)
            {
                throw new ArgumentNullException("toDriver");
            }

            return toDriver(parent.GetChildElementByAutomationId(automationId));
        }

        public static AutomationElement GetChildElementByAutomationId(this IControlDriver parent, String automationId)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            if (automationId == null)
            {
                throw new ArgumentNullException("automationId");
            }

            var idCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, automationId);
            return parent.GetChildElementByCondition(idCondition);
        }

        public static AutomationElement GetChildElementByName(this IControlDriver parent, String name)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var nameCondition = new PropertyCondition(AutomationElement.NameProperty, name);
            return parent.GetChildElementByCondition(nameCondition);
        }

        public static AutomationElement GetChildElementByCondition(this IControlDriver parent, Condition condition)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            var getChildElementOperation = new UiOperation<AutomationElement>(
                () => parent.Element.FindFirst(TreeScope.Descendants, condition));

            var childElement = getChildElementOperation.Invoke();

            if (childElement == null)
            {
                throw new KeyNotFoundException("Unable to find element");
            }

            return childElement;
        }

        public static void Dump(this IControlDriver driver)
        {
            Dump(driver.Element, 0);
        }

        private static void Dump(AutomationElement element, Int32 tabLevel)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (tabLevel < 0)
            {
                throw new ArgumentOutOfRangeException("tabLevel");
            }

            var indent = new String(' ', tabLevel * 2);
            Console.WriteLine("{0}-Automation ID: {1}", indent, element.Current.AutomationId);
            Console.WriteLine("{0} Name: {1}", indent, element.Current.Name);
            Console.WriteLine("{0} Class: {1}", indent, element.Current.ClassName);
            Console.WriteLine("{0} Framework ID: {1}", indent, element.Current.FrameworkId);
            Console.WriteLine("{0} Is Content Element: {1}", indent, element.Current.IsContentElement);
            Console.WriteLine("{0} Is Control Element: {1}", indent, element.Current.IsControlElement);
            Console.WriteLine("{0} Item Status: {1}", indent, element.Current.ItemStatus);
            Console.WriteLine("{0} Item Type: {1}", indent, element.Current.ItemType);
            Console.WriteLine("{0} Control Type: {1}", indent, element.Current.ControlType.ProgrammaticName);
            Console.WriteLine("{0} Patterns: {1}", indent, String.Join(Environment.NewLine + indent + "           ", element.GetSupportedPatterns().Select(p => p.ProgrammaticName).ToArray()));

            element.FindAll(TreeScope.Children, Condition.TrueCondition)
                   .Cast<AutomationElement>()
                   .ToList()
                   .ForEach(e => Dump(e, tabLevel + 1));
        }
    }
}
