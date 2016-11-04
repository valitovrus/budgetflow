using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetFlow.Models;

namespace BudgetFlow.Db
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly BudgetFlowContext _dbContext;

        public PaymentsRepository(BudgetFlowContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateNew(Payment payment)
        {
            PaymentDBO dbo = new PaymentDBO(payment);
            _dbContext.Add(dbo);
            _dbContext.SaveChanges();
            return dbo.Id;
        }

        public IEnumerable<Payment> Get()
        {
            return _dbContext.Payments;
        }
    }
}
