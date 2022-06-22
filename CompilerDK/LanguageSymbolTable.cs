using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDK
{
    public class LanguageSymbolTable
    {
        public List<Atom> Atoms { get; set; } = new List<Atom>();
        //public Atom Bool = new Atom("PR01", "Bool");
        //padrões léxicos
        //public Atom Identifier = new Atom("ID01", "");
        //public Atom Character = new Atom("ID05", "\'[a-z]\'");


        //public Atom character = new Atom("PR01", "[a-z]");
        //public Atom Digito = new Atom("PR01", "[0-9]");

        public LanguageSymbolTable()
        {
            CreateAtoms();
        }

        private void CreateAtoms()
        {
            Atom Bool = new Atom("PR01", "^bool$", "^b(o(o(l)?)?)?$");
            Atoms.Add(Bool);
            Atom While = new Atom("PR02", "^while$", "^w(h(i(l(e)?)?)?)?$");
            Atoms.Add(While);
            Atom If = new Atom("PR07", "^if$", "^if?$");
            Atoms.Add(If);

        }

    }
}
