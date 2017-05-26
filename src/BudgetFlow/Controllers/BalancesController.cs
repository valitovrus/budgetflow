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
            return _balancesRepository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]Balance value)
        {
            return _balancesRepository.CreateNew(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Balance value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _balancesRepository.Update(id, value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _balancesRepository.Delete(id);
        }
    }
}
