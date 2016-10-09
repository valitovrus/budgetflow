using BudgetFlow.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Db
{
    public class InMemoryPaymentsRepository : IPaymentsRepository
    {
        static ConcurrentDictionary<int, Payment> _dbStub = new ConcurrentDictionary<int, Payment>();

        public InMemoryPaymentsRepository()
        {
            if (_dbStub.Count == 0)
                this.CreateNew(new Payment() { Amount = 100500, Date = DateTime.Now, Frequency = PaymentFrequency.Once });
        }

        public int CreateNew(Payment payment)
        {
            int newId = _dbStub.Keys.Count + 1;
            _dbStub.TryAdd(newId, payment);
            return newId;
        }
        public IEnumerable<Payment> Get()
        {
            return _dbStub.Values;
        }
    }
}
