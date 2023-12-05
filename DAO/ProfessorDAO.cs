using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;

namespace projeto_algoritmoGenetico.DAO
{
    internal class ProfessorDAO : IDAO<Professor>
    {
        private static Conexao conexao;

        public ProfessorDAO()
        {
           conexao = new();
        }

        public List<Professor> GetAll()
        {
            List<Professor> professores = new();
            try { 
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_professor, " +
                        "   x.nome " +
                        " FROM tb_professor x ";
                    
                    MySqlDataReader reader1 = query.ExecuteReader();


                    while(reader1.Read())
                    {
                        professores.Add(new() { 
                            IdProfessor = reader1.GetInt32("id_professor"),
                            Nome = reader1.GetString("nome")
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
            return professores;
        }

        public Professor GetByID(int id)
        {
            Professor professor = new();
            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_professor, " +
                        "   x.nome " +
                        " FROM tb_professor x " +
                        " WHERE " +
                        "   x.id_professor = @id_professor";
                    query.Parameters.AddWithValue("@id_professor", id);

                    MySqlDataReader reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        professor = new()
                        {
                            IdProfessor = reader.GetInt32("id_professor"),
                            Nome = reader.GetString("nome")
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
            return professor;
        }
    }
}
