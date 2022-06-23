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
        public List<int> Lines { get; set; }

    }

    public class SymbolTable
    {
        public List<Symbol> Symbols { get; set; }

        public int SearchSymbolIndex(string lexeme)
        {

            // returna 
            if (Symbols.Any(lex => lex.Lexeme == lexeme))
                return Symbols.FindIndex(lex => lex.Lexeme == lexeme);
            
            return -1;
        }

        public void GenerateSymbolTableReport(string save_path)
        {

            using (var writer = new StreamWriter($"{save_path}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(this.Symbols);
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
