using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;

namespace projeto_algoritmoGenetico.DAO
{
    internal class HorarioDAO : IDAO<Horario>
    {
        private static Conexao conexao;

        public HorarioDAO()
        {
            conexao = new Conexao();
        }

        public List<Horario> GetAll()
        {
            List<Horario> horarios = new List<Horario>();
            try
            {
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_horario, " +
                        "   x.dia_semana," +
                        "   x.horario " +
                        " FROM tb_horario x ";

                    MySqlDataReader reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        horarios.Add(new Horario()
                        {
                            IdHorario = reader.GetInt32("id_horario"),
                            DiaSemana = reader.GetInt32("dia_semana"),
                            HorarioD = reader.GetString("horario"),
                            HorarioInicio = valorHorarioInicio(reader.GetString("horario")),
                            HorarioFim = valorHorarioFim(reader.GetString("horario"))
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexao.Close();
            }
            return horarios;
        }

        public TimeSpan valorHorarioInicio(String hr)
        {
            switch (hr)
            {
                case "M1":
                    return new TimeSpan(7,30,0);
                case "M2":
                    return new TimeSpan(8, 20, 0);
                case "M3":
                    return new TimeSpan(9, 10, 0);
                case "M4":
                    return new TimeSpan(10, 20, 0);
                case "M5":
                    return new TimeSpan(11, 10, 0);
                case "M6":
                    return new TimeSpan(12,0,0);
                case "T1":
                    return new TimeSpan(13,0,0);
                case "T2":
                    return new TimeSpan(13,50,0);
                case "T3":
                    return new TimeSpan(14,40,0);
                case "T4":
                    return new TimeSpan(15,50,0);
                case "T5":
                    return new TimeSpan(16,40,0);
                case "T6":
                    return new TimeSpan(17,50,0);
                case "N1":
                    return new TimeSpan(18,40,0);
                case "N2":
                    return new TimeSpan(19,30,0);
                case "N3":
                    return new TimeSpan(20,20,0);
                case "N4":
                    return new TimeSpan(21,20,0);
                case "N5":
                    return new TimeSpan(22,10,0);
                default:
                    return new TimeSpan(0,0,0);
            }
        }

        public TimeSpan valorHorarioFim(String hr)
        {
            switch (hr)
            {
                case "M1":
                    return new TimeSpan(8, 20, 0);
                case "M2":
                    return new TimeSpan(9, 10, 0);
                case "M3":
                    return new TimeSpan(10, 0, 0);
                case "M4":
                    return new TimeSpan(11, 10, 0);
                case "M5":
                    return new TimeSpan(12, 0, 0);
                case "M6":
                    return new TimeSpan(13, 0, 0);
                case "T1":
                    return new TimeSpan(13, 50, 0);
                case "T2":
                    return new TimeSpan(14, 40, 0);
                case "T3":
                    return new TimeSpan(15, 30, 0);
                case "T4":
                    return new TimeSpan(16, 40, 0);
                case "T5":
                    return new TimeSpan(17, 50, 0);
                case "T6":
                    return new TimeSpan(18, 40, 0);
                case "N1":
                    return new TimeSpan(19, 30, 0);
                case "N2":
                    return new TimeSpan(20, 20, 0);
                case "N3":
                    return new TimeSpan(21, 10, 0);
                case "N4":
                    return new TimeSpan(22, 10, 0);
                case "N5":
                    return new TimeSpan(23, 0, 0);
                default:
                    return new TimeSpan(0, 0, 0);
            }
        }

        Horario IDAO<Horario>.GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
