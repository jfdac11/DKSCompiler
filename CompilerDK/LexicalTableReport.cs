using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDK
{
    internal class LexicalItemTable
    {
        public string Lexeme { get; set; }
        public string AtomCode { get; set; }
        public int SymbolTableIndex { get; set; }

        public LexicalItemTable(string lexeme, string atomCode, int symbolTableIndex)
        {
            Lexeme = lexeme;
            AtomCode = atomCode;
            SymbolTableIndex = symbolTableIndex;
        }
    }

    internal class LexicalTableReport
    {
        private string[] Header =
        {
            "Davi Machado Costa",
            "75998712714",
            "davi.costa@aln.senaicimatec.edu.br",
            "Fernanda Vitória Nascimento Lisboa",
            "71981578181",
            "fernanda.lisboa@aln.senaicimatec.edu.br",
            "João Felipe de Araújo Caldas",
            "71981347724",
            "joao.caldas@aln.senaicimatec.edu.br",
            "Maria Antônia Amado Lima",
            "71992112935",
            "maria.lima@aln.senaicimatec.edu.br"
        };

        private string[] ColumnsName = { "LEXEME", "CODIGO ATOMO", "INDICE TABELA DE SIMBOLOS" };
        public List<LexicalItemTable> FoundedAtoms { get; set; } = new List<LexicalItemTable>();

        private string GetHeader()
        {
            string header = "";
            foreach (string h in Header)
                header += $"{h}\n";

            return header;
        }

        private string GetColumnsName()
        {
            string header = "";
            foreach (string hd in ColumnsName)
                header += $"{hd}\t";

            return header;
        }

        public void GenerateLexicalTableReport(string fileName, string savePath)
        {
            CultureInfo br = new CultureInfo("br-BR");

            string title = "Relatório da Análise Léxica";
            DateTime date = DateTime.Now;

            string description = $"{date.ToString("u", br)}-{fileName}.LEX";

            StreamWriter sw = new StreamWriter(Path.Combine(savePath, $"{fileName}.LEX"), false, Encoding.ASCII);
            sw.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            sw.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (description.Length / 2)) + "}", description));
            sw.Write(GetHeader());

            sw.WriteLine(String.Format("{0, 35} | {1, 25} | {2, 30} |", ColumnsName[0], ColumnsName[1], ColumnsName[2]));

            foreach(LexicalItemTable l in FoundedAtoms)
            {
                sw.WriteLine(String.Format("{0, 35} | {1, 25} | {2, 30} |", l.Lexeme, l.AtomCode, l.SymbolTableIndex.ToString()));
            }
            sw.Close();
        }

        public void ShowTableReport(string fileName)
        {
            CultureInfo br = new CultureInfo("br-BR");

            string title = "Relatório da Análise Léxica";
            DateTime date = DateTime.Now;

            string description = $"{date.ToString("u", br)}-{fileName}.LEX";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (description.Length / 2)) + "}", description));
            Console.WriteLine();
            Console.Write(GetHeader());

            Console.WriteLine(String.Format("{0, 35} | {1, 25} | {2, 30} |", ColumnsName[0], ColumnsName[1], ColumnsName[2]));

            foreach (LexicalItemTable l in FoundedAtoms)
            {
                Console.WriteLine(String.Format("{0, 35} | {1, 25} | {2, 30} |", l.Lexeme, l.AtomCode, l.SymbolTableIndex.ToString()));
            }
        }

    }
}
