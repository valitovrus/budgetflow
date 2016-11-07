using BudgetFlow.Models;
using System;
using Xunit;

namespace BudgetFlow.Tests
{
    public class BalanceTests
    {
        [Fact]
        public void CopyToCopiesAllFields() 
        {
            var balance = new Balance() { Amount = 10, Date = DateTime.Now };
            var targetBalance = new Balance();
            balance.CopyTo(targetBalance);

            Assert.Equal(balance.Amount, targetBalance.Amount);
            Assert.Equal(balance.Date, targetBalance.Date);
        }
    }
}
