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
            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Date.AddDays(-1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.Date.AddDays(10));

            Assert.Equal(1, cashFlow.Items.Count());
            Assert.Equal(balance.Amount, cashFlow.Items.First().Amount);
            Assert.Equal(balance.Date, cashFlow.Items.First().Date);
        }

        [Fact]
        public void GenerateSupportsOneTimePayments()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment = new Payment()
            {
                Amount = 2000,
                Date = DateTime.Now.Date.AddDays(1),
                Frequency = PaymentFrequency.Once,
                Name = "test"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment });

            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Date.AddDays(-1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.Date.AddDays(10));

            Assert.Equal(payment.Amount, cashFlow.Items.Last().Amount);
            Assert.Equal(payment.Amount + balance.Amount, cashFlow.Items.Last().Total);
            Assert.Equal(payment.Date, cashFlow.Items.Last().Date);
            Assert.Equal(payment.Name, cashFlow.Items.Last().Payment);
        }

        [Fact]
        public void GenerateChecksOneTimePaymentsDate()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment = new Payment()
            {
                Amount = 2000,
                Date = DateTime.Now.Date.AddDays(-10),
                Frequency = PaymentFrequency.Once,
                Name = "test"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment });

            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Date.AddDays(-1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.Date.AddDays(10));

            Assert.Equal(1, cashFlow.Items.Count());
        }

        [Fact]
        public void GenerateSupportsSeveralOneTimePayments()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment1 = new Payment()
            {
                Amount = 2000,
                Date = DateTime.Now.Date.AddDays(1),
                Frequency = PaymentFrequency.Once,
                Name = "test"
            };
            var payment2 = new Payment()
            {
                Amount = 300,
                Date = DateTime.Now.Date.AddDays(2),
                Frequency = PaymentFrequency.Once,
                Name = "test1"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment1, payment2 });

            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Date.AddDays(-1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.Date.AddDays(10));

            Assert.Collection(cashFlow.Items,
                i =>
                    {
                        Assert.Equal(balance.Amount, i.Amount);
                        Assert.Equal(balance.Amount, i.Total);
                        Assert.Equal(balance.Date, i.Date);
                        Assert.Equal("Balance", i.Payment);
                    },
                 i =>
                 {
                     Assert.Equal(payment1.Amount, i.Amount);
                     Assert.Equal(balance.Amount + payment1.Amount, i.Total);
                     Assert.Equal(payment1.Date, i.Date);
                     Assert.Equal(payment1.Name, i.Payment);
                 },
                  i =>
                  {
                      Assert.Equal(payment2.Amount, i.Amount);
                      Assert.Equal(balance.Amount + payment1.Amount + payment2.Amount, i.Total);
                      Assert.Equal(payment2.Date, i.Date);
                      Assert.Equal(payment2.Name, i.Payment);
                  }
            );
        }

        [Fact]
        public void GenerateSupportsWeeklyPayments()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment = new Payment()
            {
                Amount = 2000,
                Date = DateTime.Now.Date.AddDays(1),
                Frequency = PaymentFrequency.Weekly,
                Name = "test"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment });

            var balance = new Balance() { Amount = 1000, Date = DateTime.Now.Date.AddDays(-1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, DateTime.Now.Date.AddDays(10));

            Assert.Collection(cashFlow.Items,
                i =>
                {
                    Assert.Equal(balance.Amount, i.Amount);
                    Assert.Equal(balance.Amount, i.Total);
                    Assert.Equal(balance.Date, i.Date);
                    Assert.Equal("Balance", i.Payment);
                },
                 i =>
                 {
                     Assert.Equal(payment.Amount, i.Amount);
                     Assert.Equal(balance.Amount + payment.Amount, i.Total);
                     Assert.Equal(payment.Date.DayOfWeek, i.Date.DayOfWeek);
                     Assert.Equal(payment.Name, i.Payment);
                 },
                  i =>
                  {
                      Assert.Equal(payment.Amount, i.Amount);
                      Assert.Equal(balance.Amount + payment.Amount + payment.Amount, i.Total);
                      Assert.Equal(payment.Date.DayOfWeek, i.Date.DayOfWeek);
                      Assert.Equal(payment.Name, i.Payment);
                  }
            );
        }

        [Fact]
        public void GenerateSupportsMonthlyPayments()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment = new Payment()
            {
                Amount = 2000,
                Date = new DateTime(2016, 1, 2),
                Frequency = PaymentFrequency.Monthly,
                Name = "test"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment });

            var balance = new Balance() { Amount = 1000, Date = new DateTime(2016, 1, 1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, new DateTime(2016, 3, 1));

            Assert.Collection(cashFlow.Items,
                i =>
                {
                    Assert.Equal(balance.Amount, i.Amount);
                    Assert.Equal(balance.Amount, i.Total);
                    Assert.Equal(balance.Date, i.Date);
                    Assert.Equal("Balance", i.Payment);
                },
                 i =>
                 {
                     Assert.Equal(payment.Amount, i.Amount);
                     Assert.Equal(balance.Amount + payment.Amount, i.Total);
                     Assert.Equal(new DateTime(2016, 1, 2), i.Date);
                     Assert.Equal(payment.Name, i.Payment);
                 },
                  i =>
                  {
                      Assert.Equal(payment.Amount, i.Amount);
                      Assert.Equal(balance.Amount + payment.Amount + payment.Amount, i.Total);
                      Assert.Equal(new DateTime(2016, 2, 2), i.Date);
                      Assert.Equal(payment.Name, i.Payment);
                  }
            );

        }
        [Fact]
        public void GenerateSupportsYearlyPayments()
        {
            var payments = new Mock<IPaymentsRepository>();
            var payment = new Payment()
            {
                Amount = 2000,
                Date = new DateTime(2016, 1, 2),
                Frequency = PaymentFrequency.Yearly,
                Name = "test"
            };
            payments.Setup(p => p.Get()).Returns(new Payment[] { payment });

            var balance = new Balance() { Amount = 1000, Date = new DateTime(2016, 1, 1) };
            var generator = new CashFlowGenerator(payments.Object);

            CashFlow cashFlow = generator.Generate(balance, new DateTime(2017, 3, 1));

            Assert.Collection(cashFlow.Items,
                i =>
                {
                    Assert.Equal(balance.Amount, i.Amount);
                    Assert.Equal(balance.Amount, i.Total);
                    Assert.Equal(balance.Date, i.Date);
                    Assert.Equal("Balance", i.Payment);
                },
                 i =>
                 {
                     Assert.Equal(payment.Amount, i.Amount);
                     Assert.Equal(balance.Amount + payment.Amount, i.Total);
                     Assert.Equal(new DateTime(2016, 1, 2), i.Date);
                     Assert.Equal(payment.Name, i.Payment);
                 },
                  i =>
                  {
                      Assert.Equal(payment.Amount, i.Amount);
                      Assert.Equal(balance.Amount + payment.Amount + payment.Amount, i.Total);
                      Assert.Equal(new DateTime(2017, 1, 2), i.Date);
                      Assert.Equal(payment.Name, i.Payment);
                  }
            );

        }
    }
}
