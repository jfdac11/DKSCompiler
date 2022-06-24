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
        public List<LexicalItemTable> FoundedAtoms { get; set; } = new List<LexicalItemTable>();


    }
}
