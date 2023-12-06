using Microsoft.Win32;
using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_algoritmoGenetico.DAO
{
    internal class HorarioDisciplinaDAO : IDAO<HorarioDisciplina>
    {

        private static Conexao conexao;

        public HorarioDisciplinaDAO()
        {
            conexao = new();
        }
        public List<HorarioDisciplina> GetAll()
        {
            throw new NotImplementedException();
        }

        public HorarioDisciplina GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public HorarioDisciplina InsertHorarioDisciplina(HorarioDisciplina hd, int IdRegistroExecucao)
        {
            HorarioDisciplina horarioDisciplina = new();

            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_horario_disciplina (id_horario_disciplina, id_horario, id_disciplina, id_registro_execucao) VALUES (@id_null, @id_horario, @id_disciplina, @id_registro_execucao) ";
                    query.Parameters.AddWithValue("@id_null", null);
                    query.Parameters.AddWithValue("@id_horario", hd.IdHorario);
                    query.Parameters.AddWithValue("@id_disciplina", hd.IdDisciplina);
                    query.Parameters.AddWithValue("@id_registro_execucao", IdRegistroExecucao);

                    var result = query.ExecuteNonQuery();

                    if(result != 0)
                    {
                        query.CommandText =
                           " SELECT " +
                           "   x.id_horario_disciplina, " +
                           "   x.id_horario, " +
                           "   x.id_disciplina " +
                           " FROM tb_horario_disciplina x " +
                           " ORDER BY " +
                           "   x.id_horario_disciplina DESC " +
                           " LIMIT 1 ";

                        MySqlDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            horarioDisciplina.IdHorarioDisciplina = reader.GetInt32("id_horario_disciplina");
                            horarioDisciplina.IdDisciplina = reader.GetInt32("id_disciplina");
                            horarioDisciplina.IdHorario = reader.GetInt32("id_horario");
                        }
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

            return horarioDisciplina;
        }
    }
}
