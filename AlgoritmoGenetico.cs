using projeto_algoritmoGenetico.Classes;
using projeto_algoritmoGenetico.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace projeto_algoritmoGenetico
{
    public class AlgoritmoGenetico
    {
        private readonly List<Professor> Professores;

        private List<Horario> Horarios;

        private Double AptidaoTotal;

        readonly Stopwatch stopwatch;

        private readonly Random random = new();
        public AlgoritmoGenetico()
        {
            Professores = new ProfessorDAO().GetAll();

            Horarios = new HorarioDAO().GetAll();

            stopwatch = new();
        }

        public List<Horario> ProxGeracao()
        {
            stopwatch.Start();

            foreach(Horario horario in Horarios)
            {
                List<DisciplinaProfessor> dp = new DisciplinaProfessorDAO().GetAll();

                horario.HorarioDisciplinas = new();

                for (int i = 0; i <= 6; i++) { 
                    DisciplinaProfessor tmpDp = dp[random.Next(dp.Count)];

                    horario.HorarioDisciplinas.Add(
                        new()
                        {
                            IdDisciplina = tmpDp.IdDisciplina,
                            IdHorario = horario.IdHorario,
                            DisciplinaProfessor = tmpDp,
                        }
                    );
                }
            }

            return Horarios;
        }

        public int CalcularAptidao(Horario horario)
        {
            int conflitos = 0, compactacao = 0;
            for (int i = 0; i < horario.HorarioDisciplinas.Count; i++)
            {
                for (int j = i + 1; j < horario.HorarioDisciplinas.Count; j++)
                {
                    if (horario.HorarioDisciplinas[i].DisciplinaProfessor.IdProfessor == horario.HorarioDisciplinas[j].DisciplinaProfessor.IdProfessor)
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
                HorarioDisciplina tmpHD = horario.HorarioDisciplinas.FirstOrDefault(a => a.DisciplinaProfessor.IdProfessor == Professores[i].IdProfessor);

                int inicio = 0, fim = 0;

                if(tmpHD != null)
                {
                    inicio = (int) horario.HorarioInicio.TotalMinutes;
                    fim = (int) horario.HorarioFim.TotalMinutes;
                }

                compactacao += fim - inicio;
            }

            AptidaoTotal += (10 * conflitos + 5 + 2 * compactacao);

            return (10 * conflitos + 5 + 2 * compactacao);
        }

        public List<Horario> Selecao()
        {
            List<Horario> selecionados = new();

            while (selecionados.Count < Horarios.Count)
            {
                int indice1 = random.Next(Horarios.Count);
                int indice2 = random.Next(Horarios.Count);

                Horario individuo1 = Horarios[indice1];
                Horario individuo2 = Horarios[indice2];

                Horario vencedor = CalcularAptidao(individuo1) < CalcularAptidao(individuo2) ? individuo1 : individuo2;

                selecionados.Add(vencedor);
            }

            try
            {
                RegistrarExecucao();
                return selecionados;

            } catch (Exception ex)
            {
                throw new(ex.Message, ex);
            }

        }

        public Horario Cruzamento(Horario pai, Horario mae)
        {
            int pontoCorte = random.Next(pai.HorarioDisciplinas.Count);

            List<HorarioDisciplina> aulasFilho = pai.HorarioDisciplinas.Take(pontoCorte).ToList();
            aulasFilho.AddRange(mae.HorarioDisciplinas.Skip(pontoCorte));

            return new Horario { HorarioDisciplinas = aulasFilho };
        }

        public void Mutacao(Horario horario)
        {

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
            return Horarios[random.Next(Horarios.Count)];
        }

        public void GerarProximaGeracao()
        {
            List<Horario> proximaGeracao = ProxGeracao();

            List<Horario> paisSelecionados = Selecao();

            for (int i = 0; i < paisSelecionados.Count; i += 2)
            {
                Horario pai = paisSelecionados[i];
                Horario mae = paisSelecionados[i + 1];

                Horario descendente = Cruzamento(pai, mae);

                proximaGeracao.Add(descendente);
            }

            proximaGeracao.Add(Cruzamento(proximaGeracao[random.Next(proximaGeracao.Count)], proximaGeracao[random.Next(proximaGeracao.Count)]));
            Mutacao(proximaGeracao[random.Next(proximaGeracao.Count)]);

            Horarios = proximaGeracao;
        }


        public void RegistrarExecucao()
        {
            try
            {
                RelatorioDAO rel = new();
                HorarioDisciplinaDAO hdDAO = new();
                DisciplinaProfessorDAO dpDAO = new();
                
                stopwatch.Stop();
                RegistroExecucao registro = rel.InsertRegistroExecucao(stopwatch.ElapsedMilliseconds, AptidaoTotal);

                foreach(Horario horario in Horarios)
                {
                    foreach (HorarioDisciplina horDisc in horario.HorarioDisciplinas)
                    {
                        HorarioDisciplina hd = hdDAO.InsertHorarioDisciplina(horDisc);

                        DisciplinaProfessor dp = dpDAO.DPById( -99999, horDisc.IdDisciplina, -99999);

                        rel.InsertRegistroHorarios(registro, dp.IdDisciplinaProfessor, hd.IdHorarioDisciplina);
                    }
                }
            } catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

    }
}
