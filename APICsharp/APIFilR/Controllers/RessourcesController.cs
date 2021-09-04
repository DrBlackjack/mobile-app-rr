using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using APIFilR.Context;
using APIFilR.Helpers;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace APIFilR
{
    [ApiController]
    [Route("[controller]")]
    public class RessourcesController : Controller
    {
        public IConfiguration _configure;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _environment;

        public RessourcesController(IConfiguration configure, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _configure = configure;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<bool> PostTodoItem()
        {
            using MainContext ctx = new MainContext();
            var util = new UTILISATEUR() { id_type_compte = 1, mail = "tt", mdp = "tt", nom = "tt", prenom = "tt", verifie = 1 };
            ctx.Add(util);
            //await ctx.SaveChangesAsync();
            return Ok("1");
        }

        [HttpGet("GetTypeRelationRessource")]
        public ActionResult<type_relation_ressource> GetTypeRelationRessource()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Type_Relation_Ressource.ToList());
        }

        [HttpGet("GetCategoriesRessources")]
        public ActionResult<CATEGORIES_RESSOURCES> GetCategoriesRessources()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Categories_ressources.ToList());
        }

        [HttpGet("GetTypeRessources")]
        public ActionResult<TYPE_RESSOURCES> GetTypeRessources()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Type_Ressources.ToList());
        }

        [HttpGet("GetStatutRessource")]
        public ActionResult<type_relation_ressource> GetStatutRessource()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Statut_ressource.ToList());
        }

        [HttpGet("GetAllRessources")]
        public ActionResult<RESSOURCES> GetRessources()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Ressources.ToList());
        }

        [HttpPost("PostRessource/{email}")]
        public async Task<ActionResult<RESSOURCES>> PostRessource([FromForm] string ress,[FromForm] IFormFile image, string email)
        {
            // Check jwt token
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);            
                if (!TokenHelper.ValidateToken(token)) return BadRequest("Mauvais login");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { status = "error", message = "Vous devez vous connecter pour publier une ressource" });
            }
            // On post la resource
            CreateRessource res = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateRessource>(ress);
            using MainContext ctx = new MainContext();
            var utilisateur = ctx.utilisateur.First(t => t.mail == email);

            var ressource = new RESSOURCES()
            {
                titre_ressource = res.titreRessource,
                description_ressource = res.descriptionRessource,
                date_creation_ressource = DateTime.Now,
                chemin_document = "",
                id_categories = res.idCategories,
                id_statut = res.idStatut,
                id_type = res.idType,
                id_utilisateur = utilisateur.id_utilisateur,
            };

            ctx.Ressources.Add(ressource);
            await ctx.SaveChangesAsync();
            try {
                //On post l'image
                string nom = Regex.Replace(image.FileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                int id = ressource.id_ressource;
                string filePath = Path.Combine(_environment.ContentRootPath, "Images", id + "");
                Directory.CreateDirectory(filePath);
                using (var fileStream = new FileStream(Path.Combine(filePath, nom), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                ressource.chemin_document = Path.Combine("Images", id + "", nom);
                await ctx.SaveChangesAsync();
                return Ok(ctx.Ressources.ToList());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Json(new { status = "error", message = "ca me pete les couilles" }); 
            }
        }

        [HttpGet("GetCommentaire/{idRessource}")]
        public ActionResult<CommentaireDisplay[]> GetCommentaire(int idRessource)
        {
            using MainContext ctx = new MainContext();
            var commentaires = ctx.Commentaires
            .Include(c => c.Utilisateur)
            .Where(com => com.id_ressource == idRessource).ToList()
                .Select(com =>
                {
                    return new CommentaireDisplay
                    {
                        idUtilisateur = com.id_utilisateur,
                        utilisateur = com.Utilisateur.prenom + " " + com.Utilisateur.nom,
                        commentaire = com.commentaire
                    };
                });
            return Ok(commentaires);
        }

        [HttpPost("PostCommentaire/{email}")]
        public async Task<ActionResult> PostRessource([FromBody] COMMENTAIRES com, string email)
        {
            // Check jwt token
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);            
            if (!TokenHelper.ValidateToken(token)) return BadRequest("Mauvais login");

            // On post la resource
            using MainContext ctx = new MainContext();

            com.id_utilisateur = ctx.utilisateur.First(t => t.mail == email).id_utilisateur;

            ctx.Commentaires.Add(com);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}