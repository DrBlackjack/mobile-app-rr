using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilR.Model
{
    public class type_relation_ressource
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id_relation_ressource { get; set; }
        public string lib_type_relation { get; set; }
    }
}
