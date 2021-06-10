using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using APIFilR.Context;
using APIFilR.Helpers;
using Microsoft.AspNetCore.Authentication;
using System;

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
        public async Task<ActionResult<bool>> PostTodoItem()
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
        public async Task<ActionResult<type_relation_ressource>> GetTypeRelationRessource()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {               
                return Ok(ctx.Type_Relation_Ressource.ToList());
            }            
        }

        [HttpGet("GetCategoriesRessources")]
        public async Task<ActionResult<CATEGORIES_RESSOURCES>> GetCategoriesRessources()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                return Ok(ctx.Categories_ressources.ToList());
            }
        }

        [HttpGet("GetTypeRessources")]
        public async Task<ActionResult<TYPE_RESSOURCES>> GetTypeRessources()
        {
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                return Ok(ctx.Type_Ressources.ToList());
            }
        }

        [HttpPost("PostRessource/{titre}/{contenu}/{idCategorie}/{idType}/{idStatut}/{email}")]
        public async Task<ActionResult<RESSOURCES>> PostRessource([FromBody] int[] relations, string titre, string contenu, int idType, int idCategorie, int idStatut, string email)
        {
            // Check jwt token
            string token = await HttpContext.GetTokenAsync("access_token");
            if (!TokenHelper.ValidateToken(token)) return Ok(null);

            // On post la resource
            using (UtilisateurContext ctx = new UtilisateurContext())
            {
                var utilisateur = ctx.utilisateur.First(t => t.mail == email);
                var ressource = new RESSOURCES() {
                    titre_ressource = titre,
                    description_ressource = contenu,
                    date_creation_ressource = DateTime.Now,
                    chemin_document = null,
                    id_categories = idCategorie, 
                    id_statut = idStatut,
                    id_type = idType,
                    id_utilisateur = utilisateur.id_utilisateur,
                };
                return Ok(ctx.Ressources.ToList());
            }
        }
    }
}
