using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_REST_aux_horarios_grade.Classes;
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
                        "   p.id_professor, " +
                        "   p.nome " +
                        " FROM tb_professor p ";
                    
                    MySqlDataReader reader = query.ExecuteReader();

                    while(reader.Read())
                    {
                        professores.Add(new Professor() { 
                            IdProfessor = reader.GetInt32("id_professor"),
                            Nome = reader.GetString("nome")
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
