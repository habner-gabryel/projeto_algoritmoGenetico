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
            conexao = new();
        }

        public List<Horario> GetAll()
        {
            List<Horario> horarios = new();
            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

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
                        horarios.Add(new()
                        {
                            IdHorario = reader.GetInt32("id_horario"),
                            DiaSemana = reader.GetInt32("dia_semana"),
                            HorarioD = reader.GetString("horario"),
                            HorarioInicio = ValorHorarioInicio(reader.GetString("horario")),
                            HorarioFim = ValorHorarioFim(reader.GetString("horario"))
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new(ex.Message, ex);
            }
            finally
            {
                conexao.Close();
            }
            return horarios;
        }

        public static TimeSpan ValorHorarioInicio(String hr)
        {
            return hr switch
            {
                "M1" => new TimeSpan(7, 30, 0),
                "M2" => new TimeSpan(8, 20, 0),
                "M3" => new TimeSpan(9, 10, 0),
                "M4" => new TimeSpan(10, 20, 0),
                "M5" => new TimeSpan(11, 10, 0),
                "M6" => new TimeSpan(12, 0, 0),
                "T1" => new TimeSpan(13, 0, 0),
                "T2" => new TimeSpan(13, 50, 0),
                "T3" => new TimeSpan(14, 40, 0),
                "T4" => new TimeSpan(15, 50, 0),
                "T5" => new TimeSpan(16, 40, 0),
                "T6" => new TimeSpan(17, 50, 0),
                "N1" => new TimeSpan(18, 40, 0),
                "N2" => new TimeSpan(19, 30, 0),
                "N3" => new TimeSpan(20, 20, 0),
                "N4" => new TimeSpan(21, 20, 0),
                "N5" => new TimeSpan(22, 10, 0),
                _ => new TimeSpan(0, 0, 0),
            };
        }

        public static TimeSpan ValorHorarioFim(String hr)
        {
            return hr switch
            {
                "M1" => new TimeSpan(8, 20, 0),
                "M2" => new TimeSpan(9, 10, 0),
                "M3" => new TimeSpan(10, 0, 0),
                "M4" => new TimeSpan(11, 10, 0),
                "M5" => new TimeSpan(12, 0, 0),
                "M6" => new TimeSpan(13, 0, 0),
                "T1" => new TimeSpan(13, 50, 0),
                "T2" => new TimeSpan(14, 40, 0),
                "T3" => new TimeSpan(15, 30, 0),
                "T4" => new TimeSpan(16, 40, 0),
                "T5" => new TimeSpan(17, 50, 0),
                "T6" => new TimeSpan(18, 40, 0),
                "N1" => new TimeSpan(19, 30, 0),
                "N2" => new TimeSpan(20, 20, 0),
                "N3" => new TimeSpan(21, 10, 0),
                "N4" => new TimeSpan(22, 10, 0),
                "N5" => new TimeSpan(23, 0, 0),
                _ => new TimeSpan(0, 0, 0),
            };
        }

        public Horario GetByID(int id)
        {
            Horario horario = new();
            try
            {

                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_horario, " +
                        "   x.dia_semana," +
                        "   x.horario " +
                        " FROM tb_horario x " +
                        " WHERE " +
                        "   x.id_horario = @id_horario";
                    query.Parameters.AddWithValue("@id_horario", id);

                    MySqlDataReader reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        horario = new()
                        {
                            IdHorario = reader.GetInt32("id_horario"),
                            DiaSemana = reader.GetInt32("dia_semana"),
                            HorarioD = reader.GetString("horario"),
                            HorarioInicio = ValorHorarioInicio(reader.GetString("horario")),
                            HorarioFim = ValorHorarioFim(reader.GetString("horario"))
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                throw new(ex.Message, ex);
            }
            finally
            {
                conexao.Close();
            }
            return horario;
        }
    }
}
