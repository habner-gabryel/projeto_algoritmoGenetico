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
           conexao = new Conexao();
        }

        public List<Professor> GetAll()
        {
            List<Professor> professores = new List<Professor>();
            try { 
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_professor, " +
                        "   x.nome " +
                        " FROM tb_professor x ";
                    
                    MySqlDataReader reader1 = query.ExecuteReader();


                    while(reader1.Read())
                    {
                        query.CommandText = 
                            " select " +
                            "   x.id_horario_professor, " +
                            "   x.id_horario, " +
                            "   x.id_professor " +
                            " from tb_horario_professor x " +
                            " where " +
                            "   x.id_professor = @id_professor ";
                        query.Parameters.AddWithValue("@id_professor", reader1.GetInt32("id_professor"));

                        MySqlDataReader reader2 = query.ExecuteReader();

                        List<HorarioProfessor> horProf = new();

                        while(reader2.Read()) {
                            horProf.Add(new HorarioProfessor()
                            {
                                IdHorarioProfessor = reader2.GetInt32("id_horario_professor"),
                                IdHorario = reader2.GetInt32("id_horario"),
                                IdProfessor = reader2.GetInt32("id_professor")
                            });
                        }

                        professores.Add(new Professor() { 
                            IdProfessor = reader1.GetInt32("id_professor"),
                            Nome = reader1.GetString("nome"),
                            HorariosProfessor = horProf
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
            return professores;
        }

        public Professor GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
