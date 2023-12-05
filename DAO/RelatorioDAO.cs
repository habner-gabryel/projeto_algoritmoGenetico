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
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_registro_execucao, " +
                        "   x.tempo_execucao, " +
                        "   x.aptidao " +
                        " FROM tb_registro_execucao x ";

                    MySqlDataReader reader1 = query.ExecuteReader();

                    while (reader1.Read())
                    {
                        query.CommandText =
                            " SELECT " +
                            "   x.id_registro_horarios, " +
                            "   x.id_registro_execucao, " +
                            "   x.id_disciplina_professor, " +
                            "   x.id_horario_disciplina," +
                            "   hd.id_horario " +
                            " FROM tb_registro_horarios x " +
                            "   INNER JOIN tb_horario_disciplina hd ON (hd.id_horario_disciplina = x.id_horario_disciplina) " +
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
                                IdDisciplinaProfessor = reader2.GetInt32("id_disciplina_professor"),
                                IdHorarioDisciplina = reader2.GetInt32("id_horario_disciplina"),
                                IdHorario = reader2.GetInt32("id_horario")
                            });
                        }

                        relatorios.Add(new Relatorio()
                        {
                            IdRegistroExecucao = reader1.GetInt32("id_registro_execucao"),
                            TempoExecucao = reader1.GetInt64("tempo_execucao"),
                            Aptidao = reader1.GetDouble("aptidao"),
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

        public RegistroExecucao InsertRegistroExecucao(BigInteger tempo, Double aptidao)
        {
            RegistroExecucao registro = new();
            try
            {
                if(conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_registro_execucao (id_registro_execucao, tempo_execucao, aptidao) VALUES (DEFAULT, @tempo, @aptidao) ";
                    query.Parameters.AddWithValue("@tempo", tempo);
                    query.Parameters.AddWithValue("@aptidao", aptidao);

                    var result = query.ExecuteNonQuery();

                    if(result > 0)
                    {
                        query.CommandText =
                            " SELECT " +
                            "   x.id_registro_execucao, " +
                            "   x.tempo_execucao, " +
                            "   x.aptidao " +
                            " FROM tb_registro_execucao x " +
                            " ORDER BY " +
                            "   id_registro_execucao DESC " +
                            " LIMIT 1 ";

                        MySqlDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            registro = new() {
                                IdRegistroExecucao = reader.GetInt32("id_registro_execucao"),
                                TempoExecucao = reader.GetInt64("tempo_execucao"),
                                Aptidao = reader.GetDouble("aptidao")
                            }; 
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
            return registro;
        }

        public void InsertRegistroHorarios(RegistroExecucao registro, int IdDisciplinaProfessor, int IdHorarioDisciplina)
        {
            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_registro_horarios (id_disciplina_professor, id_horario_disciplina, id_registro_execucao) VALUES (@id_disciplina_professor, @id_registro_execucao)";
                    query.Parameters.AddWithValue("@id_disciplina_professor", IdDisciplinaProfessor);
                    query.Parameters.AddWithValue("@id_horario_disciplina", IdHorarioDisciplina);
                    query.Parameters.AddWithValue("@id_registro_execucao", registro.IdRegistroExecucao);

                    var result = query.ExecuteNonQuery();
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
        }

        public Relatorio GetByID(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
