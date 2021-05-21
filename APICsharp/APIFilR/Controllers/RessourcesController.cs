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
    public class RessourcesController : Controller
    {
        public IConfiguration _configure;

        public RessourcesController(IConfiguration configure)
        {
            _configure = configure;
        }

        [HttpGet]
        public async Task<ActionResult<UTILISATEUR>> PostTodoItem()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                var util = new UTILISATEUR() { id_type_compte = 1, mail = "tt", mdp = "tt", nom = "tt", prenom = "tt", verifie = 1 };
                ctx.Add(util);
                //await ctx.SaveChangesAsync();
                return Ok("1");

            }
        }

        [HttpGet("GetTypeRelationRessource")]
        public async Task<ActionResult<UTILISATEUR>> GetTypeRelationRessource()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
               
                return Ok(ctx.Type_Relation_Ressource.ToList());
            }            
        }

        [HttpGet("GetCategoriesRessources")]
        public async Task<ActionResult<UTILISATEUR>> GetCategoriesRessources()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {

                return Ok(ctx.Categories_ressources.ToList());
            }
        }

        [HttpGet("GetTypeRessources")]
        public async Task<ActionResult<UTILISATEUR>> GetTypeRessources()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {

                return Ok(ctx.Type_Ressources.ToList());
            }
        }

        [HttpPost("PostRessource/{titre}/{contenu}/{idCategorie}/{idStatut}/{email}")]
        public async Task<ActionResult<UTILISATEUR>> PostRessource([FromBody] int[] relations, string titre, string contenu, int idCategorie, int idStatut, string email)
        {
            // TODO : check jwt token
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                var idUtil = ctx.utilisateur.First(t => t.mail == email).mail;
                var ressource = new RESSOURCES() {
                    titre_ressource = titre,
                    description_ressource = contenu,
                    id_categories = idCategorie, 
                    id_statut = idStatut, 
                    };
                return Ok(ctx.Ressources.ToList());
            }
        }
    }
}
