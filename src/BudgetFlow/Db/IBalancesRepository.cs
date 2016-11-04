using BudgetFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Db
{
    public interface IBalancesRepository
    {
        int CreateNew(Balance payment);
        IEnumerable<Balance> Get();
    }
}
