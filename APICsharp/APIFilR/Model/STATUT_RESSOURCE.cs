using System.Collections.Generic;

namespace APIFilR.Model
{
	public class STATUT_RESSOURCE
	{
		public STATUT_RESSOURCE()
		{
			ListRessources = new List<RESSOURCES>();
		}

		[System.ComponentModel.DataAnnotations.Key]
		public int id_statut { get; set; }
		public string lib_statut { get; set; }

		public virtual List<RESSOURCES> ListRessources { get; set; }

		public StatutDisplay ToDisplay() => new StatutDisplay 
		{ 
			id_statut = id_statut,
			lib_statut = lib_statut
		};
		
	}
}