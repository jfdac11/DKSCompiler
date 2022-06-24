using System;
using System.Collections.Generic;
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

        private string[] ColumnsName = { "LEXEME\t", "CODIGO ATOMO\t", "INDICE TABELA DE SIMBOLOS\t" };
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
                header += hd;

            return header;
        }

        public void GenerateLexicalTableReport(string file_name, string save_path = @"E:\Projetos\Faculdade\DKSCompiler\CompilerDK\teste.dks")
        {
            // mudar depois de .txt para .LEX
            StreamWriter sw = new StreamWriter(Path.Combine(save_path, $"{file_name}.txt"), true, Encoding.ASCII);

            sw.WriteLine(GetColumnsName());
            foreach(LexicalItemTable l in FoundedAtoms)
            {
                sw.Write($"\n{l.Lexeme}");
                sw.Write($"\t{l.AtomCode}");
                sw.Write($"\t{l.SymbolTableIndex.ToString()}");
            }
            sw.Close();
        }

        public void ShowTableReport()
        {
            Console.WriteLine(GetColumnsName);
            foreach(LexicalItemTable l in FoundedAtoms)
            {
                string txt = $"{l.Lexeme}\t{l.AtomCode}\t{l.SymbolTableIndex.ToString()}";
                Console.WriteLine(txt);
            }
        }

    }
}
