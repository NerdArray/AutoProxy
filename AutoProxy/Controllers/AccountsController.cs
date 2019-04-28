using AutoProxy.Models;
using AutoProxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoProxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountsController : ApiControllerBase
    {
        private readonly AutotaskService _atService;

        public AccountsController(IServiceProvider services)
        {
            _atService = services.GetRequiredService<AutotaskService>();
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string filter)
        {
            var result = await _atService.Query<Account>(ParseFilters(filter));
            return Ok(result);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _atService.Query<Account>(ParseFilters("id equals " + id));
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
        }
    }
}
