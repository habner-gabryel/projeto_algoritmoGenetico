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

        [Display(Name = "HoraInicio")]
        [Column("hora_inicio")]
        public DateTime HoraInicio { get; set;}

        [Display(Name = "HoraFim")]
        [Column("hora_fim")]
        public DateTime HoraFim { get; set; }
    }
}
