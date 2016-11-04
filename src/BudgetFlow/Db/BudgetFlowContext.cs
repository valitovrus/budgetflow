using BudgetFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Db
{
    public class BudgetFlowContext : DbContext
    {
        public BudgetFlowContext(DbContextOptions<BudgetFlowContext> options)
            : base(options)
        {
        }


        public DbSet<PaymentDBO> Payments { get; set; }
        public DbSet<BalanceDBO> Balances { get; set; }
    }

    public class PaymentDBO : Payment
    {
        public PaymentDBO()
        {

        }
        public PaymentDBO(Payment payment)
        {
            payment.CopyTo(this);
        }

        public int Id { get; set; }
    }
    public class BalanceDBO : Balance
    {
        public BalanceDBO()
        {

        }
        public BalanceDBO(Balance payment)
        {
            payment.CopyTo(this);
        }

        public int Id { get; set; }
    }
}
