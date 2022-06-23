using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerDK
{
    public class LanguageSymbolTable
    {
        public List<Atom> Atoms { get; set; } = new List<Atom>();
        public Regex LanguageCharacterValidator = new Regex("[a-z]|[0-9]|\"|!|=|<|>|#|&|\\(|\\)|;|\\[|\\]|{|}|,|%|\\/|\\*|\\+|\\||-|\\s|\\$|\\.|_|\'");
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

            Atom Identifier = new Atom("ID01", "^(([a-zA-Z]|_)+[0-9]*)+$", "^(([a-zA-Z]|_)+[0-9]*)+$");
            Atoms.Add(Identifier);
            Atom ConstantString = new Atom("ID02", "^\"([a-zA-Z]|\\s|[0-9]|\\$|_|\\.)+\"$", "^\"(([a-zA-Z]|\\s|[0-9]|\\$|_|\\.)+\"?)?$");
            Atoms.Add(ConstantString);
            Atom IntegerNumber = new Atom("ID03", "^[0-9]+$", "^[0-9]+$");
            Atoms.Add(IntegerNumber);
            Atom Function = new Atom("ID04", "^([a-zA-Z]+[0-9]*)+$", "^([a-zA-Z]+[0-9]*)+$");
            Atoms.Add(Function);
            Atom Character = new Atom("ID05", "^'[a-zA-Z]'$", @"^'([a-zA-Z]'?)?$");
            Atoms.Add(Character);
            Atom FloatNumber = new Atom("ID06", @"^[0-9]+\.[0-9]+(e(-|\+)?[0-9]+)?$", @"^[0-9]+(\.([0-9]+((e((-|\+)?[0-9]*)?)?)?)?)?$");
            Atoms.Add(FloatNumber);
        }

    }
}