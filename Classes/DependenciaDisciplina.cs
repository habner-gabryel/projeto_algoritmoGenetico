using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_dependencia_disciplina")]
    public class DependenciaDisciplina
    {
        [Display(Name = "IdDependenciaDisciplina")]
        [Column("id_dependencia_disciplina")]
        public int IdDependenciaDisciplina { get; set; }

        [Display(Name = "IdDisciplinaDependente")]
        [Column("id_disciplina_dependente")]
        public int IdDisciplinaDependente { get; set; }

        [Display(Name = "IdDisciplinaNecessaria")]
        [Column("id_disciplina_necessaria")]
        public int IdDisciplinaNecessaria { get; set; }

    }
}
