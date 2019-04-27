using AutoProxy.Models;
using AutoProxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoProxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AutotaskService _atService;

        public AccountsController(IServiceProvider services)
        {
            _atService = services.GetRequiredService<AutotaskService>();
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<QueryResult<Account>> Get(int id)
        {
            var result = await _atService.Query<Account>(new string[] { "id,equals," + id });
            return result;
        }

        // POST: api/Accounts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
