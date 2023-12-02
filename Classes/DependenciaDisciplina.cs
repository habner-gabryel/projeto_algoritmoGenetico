using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    public class DependenciaDisciplina
    {
        public int IdDependenciaDisciplina { get; set; }

        public int IdDisciplinaDependente { get; set; }

        public int IdDisciplinaNecessaria { get; set; }

    }
}
