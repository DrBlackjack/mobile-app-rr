namespace APIFilR.Model
{
	public class COMMENTAIRES
	{
		public int id_commentaire { get; set; }
		public int id_ressource { get; set; }
		public string commentaire { get; set; }
		public int id_utilisateur { get; set; }
	}
}