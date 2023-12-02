using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    public class HorarioDisciplina
    {
        public int IdHorarioDisciplina { get; set; }

        public int IdHorario { get; set; }

        public int IdDisciplina { get; set; }

        public Disciplina Disciplina { get; set; }
        public DisciplinaProfessor DisciplinaProfessor { get; set; }
    }
}
