using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

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

    }

    public class SymbolTable
    {
        public List<Symbol> Symbols { get; set; } = new List<Symbol>();


        public int AddSymbolToTable(Symbol symbol)
        {
            this.Symbols.Add(symbol);

            return this.Symbols.Count() - 1;
        }

        public int SearchSymbolIndex(string lexeme)
        {

            if (Symbols.Any(lex => lex.Lexeme == lexeme))
                return Symbols.FindIndex(lex => lex.Lexeme == lexeme);
            
            return -1;
        }

        public void UpdateSymbolTable(Symbol symbol)
        {
            Symbols.Where(sym => sym.Lexeme.Equals(symbol.Lexeme) &&
                                            sym.Atom == symbol.Atom
                                          )
                            .ToList().ForEach(s => s.Lines.Add(symbol.Lines[0]));
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

        private string[] GetHeaderTable()
        {
            string[] header_table =
            {
                    "Tabela de Símbolos", "\n\n", "ENTRADA\t", "CODIGO\t", "LEXEME\t", "QUANTIDADE_ANTES\t", "QUANTIDADE_DEPOIS\t", "TIPO\t", "5_PRIMEIRAS_LINHAS\t"
            };

            return header_table;
        }

        public void ShowSymbolTableItems()
        {
                        
            foreach (string header in GetHeaderTable())
                Console.Write(header);

            Console.WriteLine("\n");
            foreach (Symbol symbol in Symbols)
            {
                string first_lines = GetLines(symbol.Lines.Take(5).ToList());
                
                string item = $"{Symbols.IndexOf(symbol).ToString()}\t{symbol.Atom.Code}\t{symbol.Lexeme}\t{symbol.LengthBeforeTruncation.ToString()}\t{symbol.LengthAfterTruncation.ToString()}\t{symbol.Type}\t{first_lines}";

                Console.WriteLine(item);
            }
        }

        public void GenerateSymbolTableReport(string save_path, string type)
        {
            CultureInfo br = new CultureInfo("br-BR");
            if (type.ToLower().Equals("csv"))
            {
                using (var writer = new StreamWriter($"{save_path}.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(this.Symbols);
                }
            }
            else
            {
                // por enquanto gravar no formato .txt para depois gravar em .TAB
                StreamWriter sw = new StreamWriter(Path.Combine(save_path, "symbol_table_report.txt"), true, Encoding.ASCII);

                DateTime date = DateTime.Now;

                string[] identifier_lines =
                {
                    "Relatório da Tabela de Símbolos", date.ToString("u", br), "\n\n"
                };
                                

                sw.WriteLine(identifier_lines);
                sw.WriteLine(GetHeaderTable());
                
                foreach(Symbol symbol in Symbols)
                {
                    string first_lines = GetLines(symbol.Lines.Take(5).ToList());

                    string item = $"{Symbols.IndexOf(symbol).ToString()}\t{symbol.Atom.Code}\t{symbol.Lexeme}\t{symbol.LengthBeforeTruncation.ToString()}\t{symbol.LengthAfterTruncation.ToString()}\t{symbol.Type}\t{first_lines}";
                    sw.WriteLine(item);
                }

                //await File.WriteAllLinesAsync($"{save_path}/symbol_table_report.txt", lines);
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
