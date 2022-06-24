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
        private string[] HeaderTable = { "LEXEME\t", "CODIGO ATOMO\t", "INDICE TABELA DE SIMBOLOS\t" };
        public List<LexicalItemTable> FoundedAtoms { get; set; } = new List<LexicalItemTable>();

        private string GetHeaderTable()
        {
            string header = "";
            foreach (string hd in HeaderTable)
                header += hd;

            return header;
        }

        public void GenerateLexicalTableReport(string save_path = @"E:\Projetos\Faculdade\DKSCompiler\CompilerDK\teste.dks")
        {
            // mudar depois de .txt para .LEX
            StreamWriter sw = new StreamWriter(Path.Combine(save_path, "lexical_table_report.txt"), true, Encoding.ASCII);

            sw.WriteLine(GetHeaderTable());
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
            Console.WriteLine(GetHeaderTable);
            foreach(LexicalItemTable l in FoundedAtoms)
            {
                string txt = $"{l.Lexeme}\t{l.AtomCode}\t{l.SymbolTableIndex.ToString()}";
                Console.WriteLine(txt);
            }
        }

    }
}
