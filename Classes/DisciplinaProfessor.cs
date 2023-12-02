using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    public class DisciplinaProfessor
    {
        public int IdDisciplinaProfessor { get; set; }

        public int IdDisciplina {  get; set; }

        public int IdProfessor { get; set; }
    }
}
