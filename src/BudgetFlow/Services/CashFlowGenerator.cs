using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetFlow.Db;
using BudgetFlow.Models;

namespace BudgetFlow.Services
{
    public class CashFlowGenerator
    {
        private IPaymentsRepository _paymentsRepository;

        public CashFlowGenerator(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;

        }

        public CashFlow Generate(Balance fromBalance, DateTime toDate)
        {
            var cashflow = new CashFlow();
            cashflow.Add("Balance", fromBalance.Date, fromBalance.Amount);
            return cashflow;
        }
    }
}
