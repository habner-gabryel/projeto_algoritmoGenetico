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

        List<Horario> IDAO<Horario>.GetAll()
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
                            HorarioD = reader.GetString("horario")
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

        Horario IDAO<Horario>.GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
