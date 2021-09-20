using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFilR.Model
{
	public class COMMENTAIRES
	{
		[System.ComponentModel.DataAnnotations.Key]
		public int id_commentaire { get; set; }
		public int id_ressource { get; set; }
		public string commentaire { get; set; }
		public int id_utilisateur { get; set; }
		[ForeignKey("id_utilisateur")]
		public virtual UTILISATEUR Utilisateur { get; set; }

		public COMMENTAIRES()
		{
		}
	}
}