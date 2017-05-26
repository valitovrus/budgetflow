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

        public Balance Get(int id)
        {
            return GetDBO(id);
        }

        private BalanceDBO GetDBO(int id)
        {
            return (from p in _dbContext.Balances
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        public void Update(int id, Balance value)
        {
            BalanceDBO updating = this.GetDBO(id);
            if (updating != null)
            {
                value.CopyTo(updating);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            BalanceDBO removing = this.GetDBO(id);
            if (removing != null)
            {
                _dbContext.Balances.Remove(removing);
                _dbContext.SaveChanges();
            }
        }

    }
}
