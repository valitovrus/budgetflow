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

            var paymentOrder = new List<Tuple<DateTime, Payment>>();


            foreach (Payment p in _paymentsRepository.Get())
            {
                if (p.Frequency == PaymentFrequency.Once && p.Date.Between(fromBalance.Date, toDate))
                {
                    paymentOrder.Add(new Tuple<DateTime, Payment>(p.Date, p));
                }
            }
            paymentOrder.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            foreach (var item in paymentOrder)
            {
                cashflow.Add(item.Item2.Name, item.Item1, item.Item2.Amount);
            }

            return cashflow;
        }
    }
}
