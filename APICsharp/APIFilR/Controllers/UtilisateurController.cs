using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APIFilR.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIFilR
{
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : Controller
    {
        public IConfiguration _configure;

        public UtilisateurController(IConfiguration configure)
        {
            _configure = configure;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<UTILISATEUR>> PostTodoItem()
        {

            var tt = Helper.ConVal("MaConnection");
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                var util = new UTILISATEUR() { id_type_compte = 1, mail = "tt", mdp = "tt", nom = "tt", prenom = "tt", verifie = 1 };
                ctx.Add(util);
                await ctx.SaveChangesAsync();
                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return Ok("1");

            }
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
