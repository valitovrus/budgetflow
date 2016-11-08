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

        internal void Add(string paymentName, DateTime date, decimal amount)
        {
            _items.Add(new CashFlowItem(paymentName, date, amount));
        }
    }
}
