using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace projeto_algoritmoGenetico.DAO
{
    internal class RelatorioDAO : IDAO<Relatorio>
    {
        private static Conexao conexao;

        public RelatorioDAO()
        {
            conexao = new Conexao();
        }

        public List<Relatorio> GetAll()
        {
            List<Relatorio> relatorios = new List<Relatorio>();
            try
            {
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_registro_execucao, " +
                        "   x.tempo_execucao " +
                        " FROM tb_registro_execucao x ";

                    MySqlDataReader reader1 = query.ExecuteReader();

                    while (reader1.Read())
                    {
                        query.CommandText =
                            " SELECT " +
                            "   x.id_registro_horarios, " +
                            "   x.id_registro_execucao, " +
                            "   x.id_disciplina_professor " +
                            " FROM tb_registro_horarios x " +
                            " WHERE" +
                            "   x.id_registro_execucao = @id_registro_execucao";
                        query.Parameters.AddWithValue("@id_registro_execucao", reader1.GetInt32("id_registro_execucao"));

                        MySqlDataReader reader2 = query.ExecuteReader();

                        List<RegistroHorario> registros = new();

                        while (reader2.Read())
                        {
                            registros.Add(new RegistroHorario()
                            {
                                IdRegistroHorarios = reader2.GetInt32("id_registro_horarios"),
                                IdRegistroExecucao = reader2.GetInt32("id_registro_execucao"),
                                IdDisciplinaProfessor = reader2.GetInt32("id_disciplina_professor")
                            });
                        }

                        relatorios.Add(new Relatorio()
                        {
                            IdRegistroExecucao = reader1.GetInt32("id_registro_execucao"),
                            TempoExecucao = reader1.GetInt64("tempo_execucao"),
                            RegistroHorarios = registros
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
            return relatorios;
        }

        public void InsertRegistroExecucao(BigInteger tempo)
        {
            try
            {
                if(conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_registro_execucao (tempo_execucao) VALUES (CAST(@tempo AS UNSIGNED))";
                    query.Parameters.AddWithValue("@tempo", tempo.ToString());

                    var result = query.ExecuteNonQuery();
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
        }

        public void InsertRegistroHorarios(RegistroHorario registro)
        {
            try
            {
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_registro_horarios (id_disciplina_professor, id_registro_execucao) VALUES (@id_disciplina_professor, @id_registro_execucao)";
                    query.Parameters.AddWithValue("@id_disciplina_professor", registro.IdDisciplinaProfessor);
                    query.Parameters.AddWithValue("@id_registro_execucao", registro.IdRegistroExecucao);

                    var result = query.ExecuteNonQuery();
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
        }

        public Relatorio GetByID(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
