using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace projeto_algoritmoGenetico.Classes
{
    internal class RegistroExecucao
    {
        public int IdRegistroExecucao { get; set; }

        public BigInteger TempoExecucao { get; set; }

        public Double Aptidao { get; set; }

        public List<RegistroHorario> RegistroHorarios { get; set; }

    }
}
