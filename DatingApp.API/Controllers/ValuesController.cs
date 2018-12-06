﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // http://localhost:5000
    public class ValuesController : ControllerBase
    {
         private readonly DataContext _context;
        public ValuesController(DataContext  context)
        {
            this._context=context;
        }
       

        // GET api/values
        [HttpGet]
        public async Task <IActionResult> GetValues()
        {
            var Values = await _context.Values.ToListAsync();
            return Ok(Values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task <IActionResult> GetValues(int id)
        {
            var Value = await _context.Values.FirstOrDefaultAsync(x=>x.ID==id);
            return Ok(Value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}