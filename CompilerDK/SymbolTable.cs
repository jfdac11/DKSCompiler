using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDK
{
    public class Symbol
    {
        public Atom Atom { get; set; }
        public string Lexeme { get; set; }
        public int LengthBeforeTruncation { get; set; }
        public int LengthAfterTruncation { get; set; }
        public string Type { get; set; }
        public List<int> Lines { get; set; } = new List<int>();
        public Symbol(Atom atom, string lexeme, int beforeTrunc, int afterTrunc, string type, List<int> lines)
        {
            Atom = atom;
            Lexeme = lexeme;
            LengthBeforeTruncation = beforeTrunc;
            LengthAfterTruncation = afterTrunc;
            Type = type;
            Lines = lines;
        }

        public Symbol()
        {
        }
    }

    public class SymbolTable
    {
        private string[] HeaderTable =
            {
                    "ENTRADA\t", "CODIGO\t", "LEXEME\t", "QUANTIDADE_ANTES\t", "QUANTIDADE_DEPOIS\t", "TIPO\t", "5_PRIMEIRAS_LINHAS\t"
            };
        public List<Symbol> Symbols { get; set; } = new List<Symbol>();


        private int AddSymbolToTable(Symbol symbol)
        {
            this.Symbols.Add(symbol);

            return this.Symbols.Count() - 1;
        }

        private int SearchSymbolIndex(Symbol symbol)
        {

            if (Symbols.Any(sym => sym.Lexeme == symbol.Lexeme && sym.Atom == symbol.Atom))
                return Symbols.FindIndex(sym => sym.Lexeme == symbol.Lexeme && sym.Atom == symbol.Atom);

            return -1;
        }

        private void UpdateSymbolTable(Symbol symbol)
        {
            Symbols.Where(sym => sym.Lexeme.Equals(symbol.Lexeme) &&
                                            sym.Atom == symbol.Atom
                                          )
                            .ToList().ForEach(s =>
                            {
                                s.LengthBeforeTruncation = symbol.LengthBeforeTruncation;
                                s.Lines.Add(symbol.Lines[0]);
                            }
                            );
        }

        public int SearchAndModifyTable(Symbol newSymbol)
        {
            int lastIndex = SearchSymbolIndex(newSymbol);

            if (lastIndex == -1)
                lastIndex = AddSymbolToTable(newSymbol);
            else
                UpdateSymbolTable(newSymbol);
            return lastIndex;
        }

        private string GetLines(List<int> lines)
        {
            string first_lines = "";

            for (int l = 0; l < lines.Count(); l++)
            {
                if (l > 0)
                    first_lines += ", ";

                first_lines += lines[l].ToString();
            }

            return first_lines;
        }

        public void ShowSymbolTableItems(string fileName)
        {

            string title = "TABELA DE SÍMBOLOS";
            DateTime date = DateTime.Now;
            CultureInfo br = new CultureInfo("br-BR");

            string description = $"{date.ToString("u", br)}-{fileName}.TAB";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (description.Length / 2)) + "}", description));

            Console.WriteLine("\n\n");

            Console.WriteLine(String.Format("{0, 5} | {1, 10} | {2, 30} | {3, 15} | {4, 4} | {5, 5} | {6, 0}", HeaderTable[0], HeaderTable[1], HeaderTable[2],
                HeaderTable[3], HeaderTable[4], HeaderTable[5], HeaderTable[6]));

            foreach (Symbol symbol in Symbols)
            {
                string first_lines = GetLines(symbol.Lines.Take(5).ToList());

                string item = $"{Symbols.IndexOf(symbol).ToString()}\t{symbol.Atom.Code}\t{symbol.Lexeme}\t{symbol.LengthBeforeTruncation.ToString()}\t{symbol.LengthAfterTruncation.ToString()}\t{symbol.Type}\t{first_lines}";

                Console.WriteLine(String.Format("{0,8} | {1,13} | {2,37} | {3,21} | {4,21} | {5,5} | {6,10}",
                                   Symbols.IndexOf(symbol).ToString(), symbol.Atom.Code, symbol.Lexeme, symbol.LengthBeforeTruncation.ToString(),
                                   symbol.LengthAfterTruncation.ToString(), symbol.Type.ToString(), first_lines));
            }
        }

        public void GenerateSymbolTableReport(string fileName, string savePath)
        {
            CultureInfo br = new CultureInfo("br-BR");

            DateTime date = DateTime.Now;

            string title = "Relatório da Tabela de Símbolos";
            string identifier_lines = $"-{date.ToString("u", br)}-{fileName}.TAB";

            try
            {
                StreamWriter sw = new StreamWriter(Path.Combine(savePath, $"{fileName}.TAB"), false, Encoding.GetEncoding("utf-8"));

                sw.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
                sw.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (identifier_lines.Length / 2)) + "}", identifier_lines));
                sw.WriteLine(String.Format("{0, 5} | {1, 10} | {2, 34} | {3, 20} | {4, 4} | {5, 5} | {6, 0}", HeaderTable[0], HeaderTable[1], HeaderTable[2],
                    HeaderTable[3], HeaderTable[4], HeaderTable[5], HeaderTable[6]));

                foreach (Symbol symbol in Symbols)
                {
                    string first_lines = GetLines(symbol.Lines.Take(5).ToList());
                    sw.WriteLine(String.Format("{0,8} | {1,13} | {2,37} | {3,21} | {4,21} | {5,5} | {6,18}",
                                       Symbols.IndexOf(symbol).ToString(), symbol.Atom.Code, symbol.Lexeme, symbol.LengthBeforeTruncation.ToString(),
                                       symbol.LengthAfterTruncation.ToString(), symbol.Type.ToString(), first_lines));
                }

                sw.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("IOException:\r\n\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:\r\n\r\n" + ex.Message);
            }

        }
    }

    /*
        A tabela de símbolos do projeto irá conter os seguintes 
        atributos: número da entrada da tabela de símbolos, código do átomo, 
        lexeme, quantidade de caracteres antes da truncagem, quantidade de 
        caracteres depois da truncagem, tipo do símbolo e as cinco primeiras 
        linhas onde o símbolo aparece.
     */
}
