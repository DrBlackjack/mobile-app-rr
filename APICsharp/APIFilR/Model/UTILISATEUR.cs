using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace APIFilR.Model
{
	public class UTILISATEUR
	{
		[System.ComponentModel.DataAnnotations.Key]
		public int id_utilisateur { get; set; }
		public string mail { get; set; }
		public string mdp { get; set; }
		public string nom { get; set; }
		public string prenom { get; set; }
		public int verifie { get; set; }
		public int id_type_compte { get; set; }
		public string token_verif { get; set; }

		public UTILISATEUR()
		{
			CommentaireList = new List<COMMENTAIRES>();
		}
		public virtual List<COMMENTAIRES> CommentaireList { get; set; }
	}
}