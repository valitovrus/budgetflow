using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Models
{
    public class CashFlowItem
    {
        private CashFlowItem _previous;

        public CashFlowItem(string paymentName, DateTime date, decimal amount)
        {
            Payment = paymentName;
            Date = date;
            Amount = amount;
        }

        public CashFlowItem(string paymentName, DateTime date, decimal amount, CashFlowItem previous)
            : this(paymentName, date, amount)
        {
            this._previous = previous;
        }

        public string Payment { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Total { get { return this.Amount + (this._previous == null ? 0m : this._previous.Total); } }
    }
}
