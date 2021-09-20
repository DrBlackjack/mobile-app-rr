namespace APIFilR.Model
{
	public class RESSOURCES
	{
		[System.ComponentModel.DataAnnotations.Key]
		public int id_ressource { get; set; }
		public string titre_ressource { get; set; }
		public string description_ressource { get; set; }
		public System.DateTime date_creation_ressource { get; set; }
		public string chemin_document { get; set; }
		public int id_type { get; set; }
		public int id_categories { get; set; }
		public int id_utilisateur { get; set; }
		public int id_statut { get; set; }

		public virtual STATUT_RESSOURCE Statut { get; set; }

		public RessourceDisplay ToDisplay() =>
			new RessourceDisplay()
			{
				id_ressource = this.id_ressource,
				titre_ressource = this.titre_ressource,
				description_ressource = this.description_ressource,
				date_creation_ressource = this.date_creation_ressource,
				chemin_document = this.chemin_document,
				id_type = this.id_type,
				id_categories = this.id_categories,
				id_utilisateur = this.id_utilisateur,
				id_statut = this.id_statut
			};			
	}
}