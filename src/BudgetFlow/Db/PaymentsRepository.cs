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

        public Payment Get(int id)
        {
            return GetDBO(id);
        }

        private PaymentDBO GetDBO(int id)
        {
            return (from p in _dbContext.Payments
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        public void Update(int id, Payment value)
        {
            PaymentDBO updating = this.GetDBO(id);
            if (updating != null)
            {
                value.CopyTo(updating);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            PaymentDBO removing = this.GetDBO(id);
            if (removing != null)
            {
                _dbContext.Payments.Remove(removing);
                _dbContext.SaveChanges();
            }
        }

    }
}
