using OfficeOpenXml;
using Org.BouncyCastle.Asn1.Mozilla;
using projeto_algoritmoGenetico.Classes;
using projeto_algoritmoGenetico.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public static void Executar()
        {
            HorarioDAO horarioDAO = new HorarioDAO();

            List<Horario> horarios = horarioDAO.GetAll();

            AlgoritmoGenetico alg = new AlgoritmoGenetico();

            alg.GerarProximaGeracao(horarios);
        }


        public static void GerarArquivo()
        {
            FileInfo novoArquivo = new FileInfo("RelatorioExecucaoAlgoritmoGenetico.xlsx");

            using (ExcelPackage pacote = new ExcelPackage(novoArquivo))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.Add("Planilha1");

                // Adicionar dados à planilha
                planilha.Cells["A1"].Value = "ID Registro Execução";
                planilha.Cells["B1"].Value = "Tempo Execução";
                planilha.Cells["C1"].Value = "ID Registro Horário";
                planilha.Cells["D1"].Value = "Disciplina";
                planilha.Cells["E1"].Value = "Professor";
                planilha.Cells["F1"].Value = "Horario";

                planilha.Cells["A2"].Value = "João";
                planilha.Cells["B2"].Value = 30;

                planilha.Cells["A3"].Value = "Maria";
                planilha.Cells["B3"].Value = 25;

                // Salvar o arquivo Excel
                pacote.Save();
            }

            MessageBox.Show("Arquivo Excel gerado com sucesso!");
        }
    }
    }
}
