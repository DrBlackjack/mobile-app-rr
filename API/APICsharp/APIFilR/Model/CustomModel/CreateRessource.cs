namespace APIFilR.Model
{
	public class CreateRessource
	{
		public string[] relations { get; set; }
		public string titreRessource{ get; set; }
        public string descriptionRessource{ get; set; }
        public int idType{ get; set; }
        public int idCategories{ get; set; }
        public int idStatut{ get; set; }
	}
}