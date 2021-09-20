using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using APIFilR.Context;
using APIFilR.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace APIFilR
{
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : Controller
    {
        public IConfiguration _configure;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilisateurController(IConfiguration configure, IHttpContextAccessor httpContextAccessor)
        {
            _configure = configure;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<UTILISATEUR> PostTodoItem()
        {
            using MainContext ctx = new MainContext();
            var util = new UTILISATEUR() { id_type_compte = 1, mail = "tt", mdp = "tt", nom = "tt", prenom = "tt", verifie = 1 };
            ctx.Utilisateur.Add(util);
            //await ctx.SaveChangesAsync();
            return Ok("1");
        }

        // GET: api/<controller>
        [HttpGet("CreateAccount/{mail}/{mdp}/{nom}/{prenom}")]
        public async Task<ActionResult<UTILISATEUR>> CreateAccount(string mail, string mdp, string nom, string prenom)
        {
            using MainContext ctx = new MainContext();
            // On check si l'utilisateur n'existe pas déjà
            if (ctx.Utilisateur.Any(t => t.mail == mail))
            {
                return BadRequest("Already exist");
            }

            // On créé un token avec un md5 du mail
            string token = string.Empty;
            using (var provider = MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(token)))
                    builder.Append(b.ToString("x2").ToLower());

                token = builder.ToString();
            }

            var util = new UTILISATEUR() { id_type_compte = 1, mail = mail, mdp = Helper.HashPassword(mdp), nom = nom, prenom = prenom, verifie = 0, token_verif = token };
            ctx.Add(util);
            await ctx.SaveChangesAsync();
            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            EmailHelper email = new EmailHelper();
            email.EmailSend(mail, prenom, token);
            return Ok(TokenHelper.Get(mail));
        }

        /*[HttpGet("VerifyAccount/{token}")]
        public async Task<ActionResult<UTILISATEUR>> VerifyAccount(string token, string mdp, string nom, string prenom)
        {
            using MainContext ctx = new MainContext();
            // On check si l'utilisateur n'existe pas déjà
            if (ctx.Utilisateur.Any(t => t.token_verif == token))
            {
                return BadRequest("Already exist");
            }

            // On créé un token avec un md5 du mail
            //var token = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(token)).ToString();

            var util = new UTILISATEUR() { id_type_compte = 1, mail = token, mdp = Helper.HashPassword(mdp), nom = nom, prenom = prenom, verifie = 0, token_verif = token };
            ctx.Add(util);
            await ctx.SaveChangesAsync();
            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            EmailHelper email = new EmailHelper();
            email.EmailSend(token, prenom, token);
            return Ok(TokenHelper.Get(token));
        }*/

        [HttpGet("Login/{mail}/{mdp}")]
        public ActionResult<UTILISATEUR> Login(string mail, string mdp)
        {
            using MainContext ctx = new MainContext();
            // On check si l'utilisateur a fournit les bons logins
            var util = ctx.Utilisateur.FirstOrDefault(t => t.mail == mail);
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

        [HttpGet("testToken")]
        public ActionResult<string> CheckToken()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);
                return Ok(TokenHelper.ValidateToken(token));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(false);
            }
        }
    }
}
