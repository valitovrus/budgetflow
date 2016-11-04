using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetFlow.Db;
using BudgetFlow.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetFlow.Controllers
{
    [Route("api/[controller]")]
    public class BalancesController : Controller
    {
        IBalancesRepository _balancesRepository;

        public BalancesController(IBalancesRepository repository)
        {
            _balancesRepository = repository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Balance> Get()
        {
            return _balancesRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Balance Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]Balance balance)
        {
            return _balancesRepository.CreateNew(balance);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Balance value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
