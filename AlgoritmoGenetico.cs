using projeto_algoritmoGenetico.Classes;
using projeto_algoritmoGenetico.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projeto_algoritmoGenetico
{
    public class AlgoritmoGenetico
    {
        private List<Professor> Professores;

        private List<Horario> Horarios;

        public AlgoritmoGenetico()
        {
            Professores = new ProfessorDAO().GetAll();

            Horarios = new HorarioDAO().GetAll();
        }

        public int CalcularAptidao(Horario horario)
        {
            int conflitos = 0, aptidao = 0, compactacao = 0;
            for (int i = 0; i < horario.HorarioDisciplinas.Count; i++)
            {
                for (int j = i + 1; j < horario.HorarioDisciplinas.Count; j++)
                {
                    if ((horario.HorarioDisciplinas[i].DisciplinaProfessor == horario.HorarioDisciplinas[j].DisciplinaProfessor).Any())
                    {
                        if (horario.HorarioDisciplinas[i].IdHorario == horario.HorarioDisciplinas[j].IdHorario)
                        {
                            conflitos++;
                        }
                    }
                }
            }

            for (int i = 0; i < Professores.Count; i++)
            {
                int inicio = horario.HorarioDisciplinas.Where(a => a.DisciplinaProfessor.IdProfessor == Professores[i].IdProfessor).Min(a => (int)new HorarioDAO().GetByID(a.IdHorario).HorarioInicio.TotalMinutes);
                int fim = horario.HorarioDisciplinas.Where(a => a.DisciplinaProfessor.IdProfessor == Professores[i].IdProfessor).Max(a => (int) new HorarioDAO().GetByID(a.IdHorario).HorarioFim.TotalMinutes);
                compactacao += fim - inicio;
            }

            aptidao = 10 * conflitos + 5 + 2 * compactacao;

            return aptidao;
        }

        public List<Horario> Selecao(List<Horario> populacao)
        {
            List<Horario> selecionados = new List<Horario>();
            Random random = new Random();

            while (selecionados.Count < populacao.Count)
            {
                int indice1 = random.Next(populacao.Count);
                int indice2 = random.Next(populacao.Count);

                Horario individuo1 = populacao[indice1];
                Horario individuo2 = populacao[indice2];

                Horario vencedor = CalcularAptidao(individuo1) < CalcularAptidao(individuo2) ? individuo1 : individuo2;

                selecionados.Add(vencedor);
            }

            return selecionados;
        }

        public Horario Cruzamento(Horario pai, Horario mae)
        {
            Random random = new Random();
            int pontoCorte = random.Next(pai.HorarioDisciplinas.Count);

            List<HorarioDisciplina> aulasFilho = pai.HorarioDisciplinas.Take(pontoCorte).ToList();
            aulasFilho.AddRange(mae.HorarioDisciplinas.Skip(pontoCorte));

            return new Horario { HorarioDisciplinas = aulasFilho };
        }

        public void Mutacao(Horario horario)
        {
            Random random = new Random();

            double probabilidadeMutacao = 0.1;

            foreach (HorarioDisciplina aula in horario.HorarioDisciplinas)
            {
                if (random.NextDouble() < probabilidadeMutacao)
                {
                    Horario newHorario = NovoHorarioAleatorio();

                    aula.IdHorario = newHorario.IdHorario;

                    horario.HorarioDisciplinas.Remove(aula);
                    newHorario.HorarioDisciplinas.Add(aula);
                }
            }
        }

        private Horario NovoHorarioAleatorio()
        {
            List<Horario> horarios = new HorarioDAO().GetAll();
            Random random = new Random();
            int indice = random.Next(horarios.Count);

            return horarios[indice];
        }
    }
}
