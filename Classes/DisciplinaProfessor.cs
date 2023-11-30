using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_disciplina_professor")]
    public class DisciplinaProfessor
    {
        [Display(Name = "IdDisciplinaProfessor")]
        [Column("id_disciplina_professor")]
        public int IdDisciplinaProfessor { get; set; }

        [Display(Name = "IdDisciplina")]
        [Column("id_disciplina")]
        public int IdDisciplina {  get; set; }

        [Display(Name = "IdProfessor")]
        [Column("id_professor")]
        public int IdProfessor { get; set; }
    }
}
