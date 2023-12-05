using Microsoft.Win32;
using OfficeOpenXml;
using projeto_algoritmoGenetico.Classes;
using projeto_algoritmoGenetico.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace projeto_algoritmoGenetico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Executar_Click(object sender, RoutedEventArgs e)
        {
            for (int i =0; i <= 500; i++)
            {
                AlgoritmoGenetico alg = new();
                alg.GerarProximaGeracao();
            }
        }


        public void GerarArquivo_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Arquivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Salvar Arquivo Excel";
            saveFileDialog.FileName = "RelatorioExecucaoAlgoritmoGenetico.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Caminho do arquivo Excel escolhido pelo usuário
                string caminhoArquivo = saveFileDialog.FileName;

                FileInfo novoArquivo = new(caminhoArquivo);

                HorarioDAO HDAO = new();
                RelatorioDAO RelDAO = new();
                DisciplinaProfessorDAO DiscProfDAO = new();
                DisciplinaDAO DiscDAO = new();
                ProfessorDAO ProfDAO = new();

                List<Horario> horarios = HDAO.GetAll();
                List<Relatorio> relatorios = RelDAO.GetAll();

                using (ExcelPackage pacote = new(novoArquivo))
                {
                    foreach (Relatorio relatorio in relatorios)
                    {
                        ExcelWorksheet planilha = pacote.Workbook.Worksheets.Add("Execucao nº " + relatorio.IdRegistroExecucao);

                        planilha.Cells["A1"].Value = "Horários";
                        planilha.Cells["B1"].Value = "Segunda-Feira";
                        planilha.Cells["C1"].Value = "Terça-Feira";
                        planilha.Cells["D1"].Value = "Quarta-Feira";
                        planilha.Cells["E1"].Value = "Quinta-Feira";
                        planilha.Cells["F1"].Value = "Sexta-Feira";

                        planilha.Cells["H1"].Value = "Tempo de Execução:";
                        planilha.Cells["I1"].Value = relatorio.TempoExecucao + " ms";

                        planilha.Cells["H2"].Value = "Aptidão:";
                        planilha.Cells["I2"].Value = relatorio.Aptidao;

                        foreach (Horario horario in horarios)
                        {
                            planilha.Cells["A" + (horario.IdHorario + 1)].Value = horario.HorarioD;
                        }

                        foreach (RegistroHorario rh in relatorio.RegistroHorarios)
                        {
                            Horario hr = HDAO.GetByID(rh.IdHorario);
                            DisciplinaProfessor dp = DiscProfDAO.DPById(rh.IdDisciplinaProfessor, -99999, -99999);
                            Disciplina disc = DiscDAO.GetByID(dp.IdDisciplina);
                            Professor prof = ProfDAO.GetByID(dp.IdProfessor);

                            switch (hr.DiaSemana)
                            {
                                case 1:
                                    planilha.Cells["B" + (hr.IdHorario + 1).ToString()].Value = prof.Nome + " - " + disc.Nome;
                                    break;
                                case 2:
                                    planilha.Cells["C" + (hr.IdHorario + 1).ToString()].Value = prof.Nome + " - " + disc.Nome;
                                    break;
                                case 3:
                                    planilha.Cells["D" + (hr.IdHorario + 1).ToString()].Value = prof.Nome + " - " + disc.Nome;
                                    break;
                                case 4:
                                    planilha.Cells["E" + (hr.IdHorario + 1).ToString()].Value = prof.Nome + " - " + disc.Nome;
                                    break;
                                case 5:
                                    planilha.Cells["F" + (hr.IdHorario + 1).ToString()].Value = prof.Nome + " - " + disc.Nome;
                                    break;
                            }
                        }
                    }
                    pacote.Save();
                }

                MessageBox.Show("Arquivo Excel gerado com sucesso!");

                try
                {
                    Process.Start(caminhoArquivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao tentar abrir o arquivo Excel: {ex.Message}");
                }

            }
        }
    }
}
