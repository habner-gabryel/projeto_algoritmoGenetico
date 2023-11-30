using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_algoritmoGenetico.Classes
{
    [Table("tb_disciplina")]
    public class Disciplina
    {
        [Display(Name = "IdDisciplina")]
        [Column("id_disciplina")]
        public int IdDisciplina { get; set; }

        [Display(Name = "NomeDisciplina")]
        [Column("nome")]
        public string Nome { get; set; }
    }
}
