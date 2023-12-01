using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projeto_algoritmoGenetico.Classes;

namespace projeto_algoritmoGenetico.DAO
{
    internal class DisciplinaDAO : IDAO<Disciplina>
    {

        private static Conexao conexao;

        public DisciplinaDAO()
        {
            conexao = new Conexao();
        }

        public List<Disciplina> GetAll()
        {
            List<Disciplina> disciplinas = new List<Disciplina>();
            try
            {
                if (conexao != null)
                {
                    var query = conexao.Query();
                    query.CommandText =
                        " SELECT " +
                        "   x.id_disciplina, " +
                        "   x.nome " +
                        " FROM tb_disciplina x ";

                    MySqlDataReader reader1 = query.ExecuteReader();


                    while (reader1.Read())
                    {
                        query.CommandText =
                            "select " +
                            "   x.id_horario_disciplina, " +
                            "   x.id_horario, " +
                            "   x.id_disciplina " +
                            "from tb_horario_disciplina x " +
                            "where " +
                            "   x.id_disciplina = @id_disciplina";
                        query.Parameters.AddWithValue("@id_disciplina", reader1.GetInt32("id_disciplina"));

                        MySqlDataReader reader2 = query.ExecuteReader();

                        List<HorarioDisciplina> horDisc = new();

                        while (reader2.Read())
                        {
                            horDisc.Add(new HorarioDisciplina()
                            {
                                IdHorarioDisciplina = reader2.GetInt32("id_horario_disciplina"),
                                IdHorario = reader2.GetInt32("id_horario"),
                                IdDisciplina = reader2.GetInt32("id_disciplina")
                            });
                        }

                        query.CommandText =
                            " select " +
                            "   x.id_dependencia_disciplina, " +
                            "   x.id_disciplina_dependente, " +
                            "   x.id_disciplina_necessaria " +
                            "from tb_dependencia_disciplina x " +
                            "where  " +
                            "   x.id_disciplina_dependente = @id_disciplina";
                        query.Parameters.AddWithValue("@id_disciplina", reader1.GetInt32("id_disciplina"));

                        reader2 = query.ExecuteReader();

                        List<DependenciaDisciplina> dpDisc = new();

                        while (reader2.Read())
                        {
                            dpDisc.Add(new DependenciaDisciplina()
                            {
                               IdDependenciaDisciplina = reader2.GetInt32("id_dependencia_disciplina"),
                               IdDisciplinaDependente = reader2.GetInt32("id_disciplina_dependente"),
                               IdDisciplinaNecessaria = reader2.GetInt32("id_disciplina_necessaria")
                            });
                        }

                        disciplinas.Add(new Disciplina()
                        {
                            IdDisciplina = reader1.GetInt32("id_disciplina"),
                            Nome = reader1.GetString("nome"),
                            HorariosDisciplina = horDisc,
                            DependenciaDisciplina = dpDisc.First<DependenciaDisciplina>()
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
            return disciplinas;
        }

        public Disciplina GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
