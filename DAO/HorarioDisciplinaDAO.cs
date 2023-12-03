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
            conexao = new Conexao();
        }
        public List<HorarioDisciplina> GetAll()
        {
            throw new NotImplementedException();
        }

        public HorarioDisciplina GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertHorarioDisciplina(HorarioDisciplina hd)
        {
            try
            {
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText = " INSERT INTO tb_horario_disciplina (id_horario, id_disciplina) VALUES (@id_horario, @id_disciplina) ";
                    query.Parameters.AddWithValue("@id_horario", hd.IdHorario);
                    query.Parameters.AddWithValue("@id_disciplina", hd.IdDisciplina);

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
    }
}
