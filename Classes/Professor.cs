using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_professor")]
    public class Professor
    {
        [Display(Name = "IdProfessor")]
        [Column("id_professor")]
        public int IdProfessor {  get; set; }

        [Display(Name = "NomeProfessor")]
        [Column("nome")]
        public string Nome { get; set; }

        public List<HorarioProfessor> HorariosProfessor { get; set; }
    }
}
