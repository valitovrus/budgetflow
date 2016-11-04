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
    }

    public class PaymentDBO : Payment
    {
        public PaymentDBO()
        {

        }
        public PaymentDBO(Payment payment)
        {
            this.Amount = payment.Amount;
            this.Date = payment.Date;
            this.Frequency = payment.Frequency;
        }

        public int Id { get; set; }
    }
}
