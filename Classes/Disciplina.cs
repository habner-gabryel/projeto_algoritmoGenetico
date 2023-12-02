using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace projeto_algoritmoGenetico.Classes
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }

        public string Nome { get; set; }

        public List<HorarioDisciplina> HorariosDisciplina { get; set; }

        public DependenciaDisciplina DependenciaDisciplina { get; set; }
    }
}
