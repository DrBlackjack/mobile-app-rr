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

		private List<COMMENTAIRES> _CommentaireList;

		public UTILISATEUR()
		{
		}

		private UTILISATEUR(ILazyLoader lazyLoader)
		{
			LazyLoader = lazyLoader;
		}
		private ILazyLoader LazyLoader { get; set; }
		public List<COMMENTAIRES> CommentaireList
		{
			get => LazyLoader.Load(this, ref _CommentaireList);
			set => _CommentaireList = value;
		}
	}
}