using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace projeto_algoritmoGenetico.DAO
{
    class DisciplinaProfessorDAO : IDAO<DisciplinaProfessor>
    {
        private readonly Conexao conexao;

        public DisciplinaProfessorDAO()
        {
            conexao = new();   
        }
        public List<DisciplinaProfessor> GetAll()
        {
            List<DisciplinaProfessor> dp = new();
            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_disciplina_professor, " +
                        "   x.id_disciplina, " +
                        "   x.id_professor " +
                        " FROM tb_disciplina_professor x ";

                    MySqlDataReader reader1 = query.ExecuteReader();

                    while (reader1.Read())
                    {
                        dp.Add(new()
                        {
                            IdDisciplinaProfessor = reader1.GetInt32("id_disciplina_professor"),
                            IdDisciplina = reader1.GetInt32("id_disciplina"),
                            IdProfessor = reader1.GetInt32("id_professor")
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
            return dp;
        }

        public DisciplinaProfessor GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public DisciplinaProfessor DPById(int idDisciplinaProfessor, int idDisciplina, int idProfessor)
        {
            DisciplinaProfessor dp = new();
            try
            {
                if (conexao != null)
                {
                    conexao.AbreConexao();

                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_disciplina_professor, " +
                        "   x.id_disciplina, " +
                        "   x.id_professor " +
                        " FROM tb_disciplina_professor x " +
                        " WHERE" +
                        "   1=1 ";
                    if(idDisciplina != -99999)
                    {
                        query.CommandText += " AND x.id_disciplina = @id_disciplina ";
                        query.Parameters.AddWithValue("@id_disciplina", idDisciplina);
                    }
                    if(idDisciplinaProfessor != -99999)
                    {
                        query.CommandText += " AND x.id_disciplina_professor = @id_disciplina_professor ";
                        query.Parameters.AddWithValue("@id_disciplina_professor", idDisciplinaProfessor);
                    }
                    if(idProfessor != -99999)
                    {
                        query.CommandText += " AND x.id_professor = @id_professor ";
                        query.Parameters.AddWithValue("@id_professor", idProfessor);
                    }

                    MySqlDataReader reader1 = query.ExecuteReader();

                    while (reader1.Read())
                    {
                        dp = new()
                        {
                            IdDisciplinaProfessor = reader1.GetInt32("id_disciplina_professor"),
                            IdDisciplina = reader1.GetInt32("id_disciplina"),
                            IdProfessor = reader1.GetInt32("id_professor")
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

            return dp;
        }
    }
}
