using BudgetFlow.Db;
using BudgetFlow.Models;
using BudgetFlow.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow.Controllers
{
    [Route("api/[controller]")]
    public class CashFlowController : Controller
    {
        IPaymentsRepository _paymentsRepository;
        IBalancesRepository _balancesRepository;
        public CashFlowController(IPaymentsRepository payments, IBalancesRepository balances)
        {
            _paymentsRepository = payments;
            _balancesRepository = balances;
        }

        [HttpGet()]
        public CashFlow Get()
        {
            var generator = new CashFlowGenerator(_paymentsRepository);


            return generator.Generate(_balancesRepository.GetLast(), DateTime.Now.AddMonths(3));
        }
    }
}
