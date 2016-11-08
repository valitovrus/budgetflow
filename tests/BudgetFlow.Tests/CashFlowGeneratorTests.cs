using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BudgetFlow.Services;
using BudgetFlow.Db;
using Moq;
using BudgetFlow.Models;

namespace BudgetFlow.Tests
{
    public class CashFlowGeneratorTests
    {
        [Fact]
        public void GenerateShouldUseLastBalance()
        {
            var payments = new Mock<IPaymentsRepository>();
            payments.Setup(p => p.Get()).Returns(Enumerable.Empty<Payment>());
            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.AddDays(10));

            Assert.Equal(1, cashFlow.Items.Count());
            Assert.Equal(balance.Amount, cashFlow.Items.First().Amount);
            Assert.Equal(balance.Date, cashFlow.Items.First().Date);

        }
    }
}
