using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Models
{
    public enum PaymentFrequency
    {
        Once,
        Weekly,
        Monthly,
        Yearly
    }

    public class Payment
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentFrequency Frequency { get; set; }
    }
}
