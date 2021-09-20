using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIFilR.Model;
using Microsoft.Extensions.Configuration;
using APIFilR.Context;
using APIFilR.Helpers;
using System;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.Net;

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
        public ActionResult<STATUT_RESSOURCE> GetStatutRessource()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Statut_ressource.Select(t=> t.ToDisplay()).ToList());
        }

        [HttpGet("GetAllRessources")]
        public ActionResult<RessourceDisplay> GetRessources()
        {
            using MainContext ctx = new MainContext();
            return Ok(ctx.Ressources.Select(t=> t.ToDisplay()).ToList());
        }
        
        [HttpGet("GetPublicRessources/{email}")]
        public ActionResult<RessourceDisplay> GetPublicRessources(string email)
        {
            using MainContext ctx = new MainContext();
            //On regarde si l'utilisateur est authentifi� et a une session valide
            var idUtil = ctx.Utilisateur.FirstOrDefault(u => u.mail == email)?.id_utilisateur;
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);
            if (idUtil != null && TokenHelper.ValidateToken(token))
            {
                //il est effectivement connect�, on peut rechercher toutes ses ressources aussi
                return Ok(ctx.Ressources.Include(r => r.Statut).Where(r => r.Statut.lib_statut == "publique" || r.id_utilisateur == idUtil).Select(t => t.ToDisplay()).ToList());
            }
            return Ok(ctx.Ressources.Include(r => r.Statut).Where(r => r.Statut.lib_statut == "publique").Select(t => t.ToDisplay()).ToList());
        }

        [HttpGet("GetFilteredRessources/{email}")]
        public ActionResult<RESSOURCES> GetRessourcesByType(string email)
        {
            using MainContext ctx = new MainContext();
            var monJson = WebUtility.UrlDecode(Request.QueryString.Value.ToString()).Split('=')[1];
            Filter res = Newtonsoft.Json.JsonConvert.DeserializeObject<Filter>(monJson);
            return Ok(ctx.Ressources.ToList()
                .Where(r => (res.idType==null? true: r.id_type == res.idType) &&
                 (res.idCategorie==null? true: r.id_categories == res.idCategorie) &&
                 (res.idStatut == null? true: r.id_statut == res.idStatut)).Select(t => t.ToDisplay()).ToList());
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
            var utilisateur = ctx.Utilisateur.First(t => t.mail == email);
            if (utilisateur.verifie != 1 )
                return Json(new { status = "error", message = "Vous devez être vérifié pour publier une ressource" });

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
                .Include(t=>t.Utilisateur)
                .Where(com => com.id_ressource == idRessource).ToList()
                .Select(com =>
                {
                    return new CommentaireDisplay
                    {
                        idUtilisateur = com.id_utilisateur,
                        utilisateur = com.Utilisateur.prenom + " " + com.Utilisateur.nom,
                        commentaire = com.commentaire
                    };
                }).ToList();
            return Ok(commentaires);
        }

        [HttpPost("PostCommentaire/{email}")]
        public async Task<ActionResult> PostRessource([FromBody] COMMENTAIRES com, string email)
        {
            // Check jwt token
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);            
            if (!TokenHelper.ValidateToken(token)) return BadRequest("Veuillez vous reconnecter");

            // On post la resource
            using MainContext ctx = new MainContext();
            var utilisateur = ctx.Utilisateur.First(t => t.mail == email);
            com.id_utilisateur = utilisateur.id_utilisateur;

            if (utilisateur.verifie != 1)
                return Json(new { status = "error", message = "Vous devez être vérifié pour commenter une ressource" });

            ctx.Commentaires.Add(com);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}