using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetFlow.Models;

namespace BudgetFlow.Db
{
    public class BalancesRepository : IBalancesRepository
    {
        private readonly BudgetFlowContext _dbContext;

        public BalancesRepository(BudgetFlowContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateNew(Balance balance)
        {
            BalanceDBO dbo = new BalanceDBO(balance);
            _dbContext.Add(dbo);
            _dbContext.SaveChanges();
            return dbo.Id;
        }

        public IEnumerable<Balance> Get()
        {
            return _dbContext.Balances;
        }
        public Balance GetLast()
        {
            return _dbContext.Balances.OrderByDescending(b => b.Date).First();
        }
    }
}
