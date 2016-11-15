using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Models
{
    public class CashFlow
    {
        List<CashFlowItem> _items = new List<CashFlowItem>();
        public IEnumerable<CashFlowItem> Items
        {
            get { return _items; }
        }
        CashFlowItem _last = null;
        public void Add(string paymentName, DateTime date, decimal amount)
        {
            var newItem = new CashFlowItem(paymentName, date, amount, _last);
            _items.Add(newItem);
            _last = newItem;
        }
        public void Sort()
        {
            _items.Sort((x, y) => x.Date.CompareTo(y.Date));
        }
    }
}
