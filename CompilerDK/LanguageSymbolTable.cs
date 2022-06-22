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
            // descomentar so depois de fazer a diferenciação por escopo
            //Atom Identifier = new Atom("ID01", "^(([a-zA-Z]|_)+[0-9]*)+$", "^(([a-zA-Z]|_)+[0-9]*)+$");
            //Atoms.Add(Identifier);
            Atom Function = new Atom("ID04", "^([a-zA-Z]+[0-9]*)+$", "^([a-zA-Z]+[0-9]*)+$");
            Atoms.Add(Function);
            Atom IntegerNumber = new Atom("ID03", "^[0-9]+$", "^[0-9]+$");
            Atoms.Add(IntegerNumber);
        }

    }
}
