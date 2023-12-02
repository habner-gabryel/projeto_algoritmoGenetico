using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Documents;

namespace projeto_algoritmoGenetico.Classes
{
    public class Horario
    {
        public int IdHorario { get; set; }


        public int DiaSemana { get; set; }

        public string HorarioD { get; set;}

        public TimeSpan HorarioInicio {  get; set; }

        public TimeSpan HorarioFim { get; set; }

        public List<HorarioDisciplina> HorarioDisciplinas { get; set; }
    }
}
