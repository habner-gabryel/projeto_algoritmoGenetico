using System.Collections.Generic;
using System.Numerics;

namespace projeto_algoritmoGenetico.Classes
{
    internal class Relatorio
    {
        public int IdRegistroExecucao { get; set; }

        public BigInteger TempoExecucao { get; set; }

        public List<RegistroHorario> RegistroHorarios { get; set; }
    }
}
