using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_horario_professor")]
    public class HorarioProfessor
    {
        [Display(Name = "IdHorarioProfessor")]
        [Column("id_horario_professor")]
        public int IdHorarioProfessor { get; set; }

        [Display(Name = "IdHorario")]
        [Column("id_horario")]
        public int IdHorario { get; set; }

        [Display(Name = "IdProfessor")]
        [Column("id_professor")]
        public int IdProfessor { get; set; }
    }
}
