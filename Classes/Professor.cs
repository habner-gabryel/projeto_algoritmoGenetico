using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    public class Professor
    {
        public int IdProfessor {  get; set; }

        public string Nome { get; set; }
    }
}
