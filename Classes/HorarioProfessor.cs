using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    public class HorarioProfessor
    {
        public int IdHorarioProfessor { get; set; }

        public int IdHorario { get; set; }

        public int IdProfessor { get; set; }
    }
}
