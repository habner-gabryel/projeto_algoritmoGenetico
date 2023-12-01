using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_horario")]
    public class Horario
    {
        [Display(Name = "IdHorario")]
        [Column("id_horario")]
        public int IdHorario { get; set; }

        [Display(Name = "DiaSemana")]
        [Column("dia_semana")]
        public int DiaSemana { get; set; }

        [Display(Name = "Horario")]
        [Column("horario")]
        public string HorarioD { get; set;}

    }
}
