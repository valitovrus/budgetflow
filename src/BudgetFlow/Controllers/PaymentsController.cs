using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetFlow.Models;
using BudgetFlow.Db;

namespace BudgetFlow.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        IPaymentsRepository _paymentsRepository;

        public PaymentsController(IPaymentsRepository repository)
        {
            _paymentsRepository = repository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return _paymentsRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Payment Get(int id)
        {
            return null;
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]Payment value)
        {
            return _paymentsRepository.CreateNew(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
