using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using APIFilR.Context;
using APIFilR.Helpers;
using Microsoft.AspNetCore.Authentication;

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
            using (MainContext ctx = new MainContext())
            {
                var util = new UTILISATEUR() { id_type_compte = 1, mail = "tt", mdp = "tt", nom = "tt", prenom = "tt", verifie = 1 };
                ctx.Add(util);
                //await ctx.SaveChangesAsync();
                return Ok("1");

            }
        }

        // GET: api/<controller>
        [HttpGet("CreateAccount/{mail}/{mdp}/{nom}/{prenom}")]
        public async Task<ActionResult<UTILISATEUR>> CreateAccount(string mail, string mdp, string nom, string prenom)
        {
            using (MainContext ctx = new MainContext())
            {
                // On check si l'utilisateur n'existe pas déjà
                if (ctx.utilisateur.Any(t=> t.mail == mail))
                {
                    return BadRequest("Already exist");
                }
                var util = new UTILISATEUR() { id_type_compte = 1, mail = mail, mdp = Helper.HashPassword(mdp), nom = nom, prenom = prenom, verifie = 0 };
                ctx.Add(util);
                await ctx.SaveChangesAsync();
                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return Ok(TokenHelper.Get(mail));
            }            
        }

        [HttpGet("Login/{mail}/{mdp}")]
        public async Task<ActionResult<UTILISATEUR>> Login(string mail, string mdp)
        {
            using (MainContext ctx = new MainContext())
            {
                // On check si l'utilisateur a fournit les bons logins
                var util = ctx.utilisateur.FirstOrDefault(t => t.mail == mail);
                if (util == null)
                {
                    // Si on ne l'a pas trouvé
                    return NotFound();
                }
                if (Helper.VerifyHash(mdp, util?.mdp))
                {
                    return Ok(TokenHelper.Get(mail));
                }
                // Mauvais pass
                return BadRequest("Wrong password");
            }
        }

        [HttpGet("testToken")]
        public async Task<ActionResult<string>> CheckToken()
        {
            try
            {
                string token = await HttpContext.GetTokenAsync("access_token");
                return Ok(TokenHelper.ValidateToken(token));
            }
            catch 
            {
                return Ok(false);
            }
        }
    }
}
