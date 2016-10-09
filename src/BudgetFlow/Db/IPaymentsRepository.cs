using BudgetFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Db
{
    public interface IPaymentsRepository
    {
        int CreateNew(Payment payment);
        IEnumerable<Payment> Get();
    }
}
