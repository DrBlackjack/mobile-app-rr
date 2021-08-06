using Microsoft.EntityFrameworkCore.Infrastructure;

namespace APIFilR.Model
{
	public class COMMENTAIRES
	{
		[System.ComponentModel.DataAnnotations.Key]
		public int id_commentaire { get; set; }
		public int id_ressource { get; set; }
		public string commentaire { get; set; }
		public int id_utilisateur { get; set; }

		private UTILISATEUR _Utilisateur;

		public COMMENTAIRES()
		{
		}

		private COMMENTAIRES(ILazyLoader lazyLoader)
		{
			LazyLoader = lazyLoader;
		}
		private ILazyLoader LazyLoader { get; set; }
		public UTILISATEUR Utilisateur
		{
			get => LazyLoader.Load(this, ref _Utilisateur);
			set => _Utilisateur = value;
		}
	}
}