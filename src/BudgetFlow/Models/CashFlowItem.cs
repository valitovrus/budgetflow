using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Models
{
    public class CashFlowItem
    {
        public CashFlowItem(string paymentName, DateTime date, decimal amount)
        {
            Payment = paymentName;
            Date = date;
            Amount = amount;
        }

        public string Payment { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
    }
}
