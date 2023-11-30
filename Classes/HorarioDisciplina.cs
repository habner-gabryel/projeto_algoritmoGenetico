using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_horario_disciplina")]
    public class HorarioDisciplina
    {
        [Display(Name = "IdHorarioDisciplina")]
        [Column("id_horario_disciplina")]
        public int IdHorarioDisciplina { get; set; }

        [Display(Name = "IdHorario")]
        [Column("id_horario")]
        public int IdHorario { get; set; }

        [Display(Name = "IdDisciplina")]
        [Column("id_disciplina")]
        public int IdDisciplina { get; set; }
    }
}
