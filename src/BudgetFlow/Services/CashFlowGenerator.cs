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
            List<Tuple<DateTime, Payment>> payments = new List<Tuple<DateTime, Payment>>();
            foreach (Payment p in _paymentsRepository.Get())
            {
                // if it looks stupid but works, it's not stupid :-\
                // it has to be rewritten, anyway
                for (var date = fromBalance.Date.AddDays(1); date <= toDate; date = date.AddDays(1))
                {
                    switch (p.Frequency)
                    {
                        case PaymentFrequency.Once:
                            if (p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year)
                                payments.Add(new Tuple<DateTime, Payment>(date, p));
                            break;
                        case PaymentFrequency.Weekly:
                            if (p.Date.DayOfWeek == date.DayOfWeek)
                                payments.Add(new Tuple<DateTime, Payment>(date, p));
                            break;
                        case PaymentFrequency.Monthly:
                            if (p.Date.Day == date.Day)
                                payments.Add(new Tuple<DateTime, Payment>(date, p));
                            break;
                        case PaymentFrequency.Yearly:
                            if (p.Date.Day == date.Day && p.Date.Month == date.Month)
                                payments.Add(new Tuple<DateTime, Payment>(date, p));
                            break;
                        default:
                            break;
                    }
                }
            }
            payments.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            foreach (var t in payments)
                cashflow.Add(t.Item2.Name, t.Item1, t.Item2.Amount);
            return cashflow;
        }
    }
}
