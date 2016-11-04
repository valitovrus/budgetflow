using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Models
{
    public class Balance
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public void CopyTo(Balance target)
        {
            target.Amount = this.Amount;
            target.Date = this.Date;
        }
    }
}
