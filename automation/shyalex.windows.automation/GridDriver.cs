using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ShyAlex.Windows.Automation
{
    public class GridDriver : IControlDriver
    {
        private readonly AutomationElement element;

        public AutomationElement Element { get { return element; } }

        public GridDriver(AutomationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
        }

        public IEnumerable<IEnumerable<AutomationElement>> GetContainedElements()
        {
            var gridPattern = (GridPattern)element.GetCurrentPattern(GridPattern.Pattern);

            for (int x = 0; x < gridPattern.Current.RowCount; x++)
            {
                yield return GetColumnValuesForRow(gridPattern, x);
            }
        }

        private IEnumerable<AutomationElement> GetColumnValuesForRow(GridPattern gridPattern, Int32 rowIndex)
        {
            for (int y = 0; y < gridPattern.Current.ColumnCount; y++)
            {
                var cellElement = gridPattern.GetItem(rowIndex, y);
                yield return TreeWalker.RawViewWalker.GetFirstChild(cellElement);
            }
        }
    }
}
