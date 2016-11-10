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

            foreach (Payment p in _paymentsRepository.Get())
            {
                // if it looks stupid but works, it's not stupid :-\
                // it has to be rewritten, anyway
                for (var date = fromBalance.Date; date <= toDate; date = date.AddDays(1))
                {
                    switch (p.Frequency)
                    {
                        case PaymentFrequency.Once:
                            if (p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year)
                                cashflow.Add(p.Name, date, p.Amount);
                            break;
                        case PaymentFrequency.Weekly:
                            if (p.Date.DayOfWeek == date.DayOfWeek)
                                cashflow.Add(p.Name, date, p.Amount);
                            break;
                        case PaymentFrequency.Monthly:
                            if (p.Date.Day == date.Day)
                                cashflow.Add(p.Name, date, p.Amount);
                            break;
                        case PaymentFrequency.Yearly:
                            if (p.Date.Day == date.Day && p.Date.Month == date.Month)
                                cashflow.Add(p.Name, date, p.Amount);
                            break;
                        default:
                            break;
                    }
                }
            }

            return cashflow;
        }
    }
}
