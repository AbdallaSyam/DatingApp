using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   // http://localhost:5000

   // andy controller in ASPCore inhirets from ControllerBase  which allow access to ex (IActionResult) not contriller like MVC
    
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
        [AllowAnonymous ]
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task <IActionResult> GetValues(int id)
        {
            var Value = await _context.Values.FirstOrDefaultAsync(x=>x.ID==id);
            return Ok(Value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Value valueobj)
        {
            var valueVar = new Value{
                Name=valueobj.Name,
                ID=7
            
            };
            _context.Add(valueVar);
            _context.SaveChanges();
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
