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
        public Regex LanguageCharacterValidator = new Regex("[a-zA-Z]|[0-9]|\"|!|=|<|>|#|&|\\(|\\)|;|\\[|\\]|{|}|,|%|\\/|\\*|\\+|\\||-|\\s|\\$|\\.|_|\'");
        private Dictionary<string, string> DefaultCodeTypes = new Dictionary<string, string>();
      
        public LanguageSymbolTable()
        {
            CreateAtoms();
            AddCodeTypes();
        }

        private void CreateAtoms()
        {
            Atom Bool = new Atom("PR01", "^bool$", "^b(o(o(l)?)?)?$");
            Atoms.Add(Bool);
            Atom While = new Atom("PR02", "^while$", "^w(h(i(l(e)?)?)?)?$");
            Atoms.Add(While);
            Atom Break = new Atom("PR03", "^break$", "^b(r(e(a(k(0)?)?)?)?)?$");
            Atoms.Add(Break);
            Atom Void = new Atom("PR04", "^void$", "^v(o(i(d)?)?)?$");
            Atoms.Add(Void);
            Atom Char = new Atom("PR05", "^char$", "^c(h(a(r)?)?)?$");
            Atoms.Add(Char);
            Atom True = new Atom("PR06", "^true$", "^t(r(u(e)?)?)?$");
            Atoms.Add(True);
            Atom If = new Atom("PR07", "^if$", "^if?$");
            Atoms.Add(If);
            Atom String = new Atom("PR08", "^string$", "^s(t(r(i(n(g)?)?)?)?)?$");
            Atoms.Add(String);
            Atom Begin = new Atom("PR09", "^begin$", "^b(e(g(i(n)?)?)?)?$");
            Atoms.Add(Begin);
            Atom Return = new Atom("PR10", "^return$", "^r(e(t(u(r(n)?)?)?)?)?$");
            Atoms.Add(Return);
            Atom False = new Atom("PR11", "^false$", "^f(a(l(s(e)?)?)?)?$");
            Atoms.Add(False);
            Atom Program = new Atom("PR12", "^program$", "^p(r(o(g(r(a(m)?)?)?)?)?)?$");
            Atoms.Add(Program);
            Atom Float = new Atom("PR13", "^float$", "^f(l(o(a(t)?)?)?)?$");
            Atoms.Add(Float);
            Atom Int = new Atom("PR14", "^int$", "^i(n(t)?)?$");
            Atoms.Add(Int);
            Atom Else = new Atom("PR15", "^else$", "^e(l(s(e)?)?)?$");
            Atoms.Add(Else);
            Atom End = new Atom("PR16", "^end$", "^e(n(d)?)?$");
            Atoms.Add(End);


            Atom Different = new Atom("SR01", "^!=$", "^!=?$");
            Atoms.Add(Different);
            Atom Hash = new Atom("SR01", "^#$", "^#$");
            Atoms.Add(Hash);
            Atom And = new Atom("SR02", "^&$", "^&$");
            Atoms.Add(And);
            Atom OpenParentheses = new Atom("SR03", "^\\($", "^\\($");
            Atoms.Add(OpenParentheses);
            Atom CloseParentheses = new Atom("SR04", "^\\)$", "^\\)$");
            Atoms.Add(CloseParentheses);
            Atom Semicolon = new Atom("SR05", "^;$", "^;$");
            Atoms.Add(Semicolon);
            Atom OpenSquareBrackets = new Atom("SR06", "^\\[$", "^\\[$");
            Atoms.Add(OpenSquareBrackets);
            Atom OpenCurlyBrackets = new Atom("SR07", "^\\{$", "^\\{$");
            Atoms.Add(OpenCurlyBrackets);
            Atom Comma = new Atom("SR08", "^,$", "^,$");
            Atoms.Add(Comma);
            Atom LessOrEqual = new Atom("SR09", "^<=$", "^<=?$");
            Atoms.Add(LessOrEqual);
            Atom Equal = new Atom("SR10", "^=$", "^=$");
            Atoms.Add(Equal);
            Atom GreaterOrEqual = new Atom("SR11", "^>=$", "^>=?$");
            Atoms.Add(GreaterOrEqual);
            Atom Exclamation = new Atom("SR12", "^!$", "^!$");
            Atoms.Add(Exclamation);
            Atom Percent = new Atom("SR13", "^%$", "^%$");
            Atoms.Add(Percent);
            Atom Slash = new Atom("SR14", "^\\/$", "^\\/$");
            Atoms.Add(Slash);
            Atom Asterisk = new Atom("SR15", "^\\*$", "^\\*$");
            Atoms.Add(Asterisk);
            Atom Plus = new Atom("SR16", "^\\+$", "^\\+$");
            Atoms.Add(Plus);
            Atom CloseSquareBrackets = new Atom("SR17", "^\\]$", "^\\]$");
            Atoms.Add(CloseSquareBrackets);
            Atom Bar = new Atom("SR18", "^\\|$", "^\\|$");
            Atoms.Add(Bar);
            Atom CloseCurlyBrackets = new Atom("SR19", "^\\}$", "^\\}$");
            Atoms.Add(CloseCurlyBrackets);
            Atom LessThan = new Atom("SR20", "^<$", "^<$");
            Atoms.Add(LessThan);
            Atom DoubleEqual = new Atom("SR21", "^==$", "^==?$");
            Atoms.Add(DoubleEqual);
            Atom GreaterThan = new Atom("SR22", "^>$", "^>$");
            Atoms.Add(GreaterThan);
            Atom Minus = new Atom("SR23", "^-$", "^-$");
            Atoms.Add(Minus);


            Atom Identifier = new Atom("ID01", "^([a-zA-Z]|_)([a-zA-Z]|_|[0-9])*$", "^([a-zA-Z]|_)([a-zA-Z]|_|[0-9])*$");
            Identifier.IsReservedWord = false;
            Atoms.Add(Identifier);
            Atom ConstantString = new Atom("ID02", "^\"([a-zA-Z]|\\s|[0-9]|\\$|_|\\.)+\"$", "^\"(([a-zA-Z]|\\s|[0-9]|\\$|_|\\.)+\"?)?$");
            ConstantString.IsReservedWord = false;
            Atoms.Add(ConstantString);
            Atom IntegerNumber = new Atom("ID03", "^[0-9]+$", "^[0-9]+$");
            IntegerNumber.IsReservedWord = false;
            Atoms.Add(IntegerNumber);
            Atom Function = new Atom("ID04", "^[a-zA-Z]([a-zA-Z]|[0-9])*$", "^[a-zA-Z]([a-zA-Z]|[0-9])*$");
            Function.IsReservedWord = false;
            Atoms.Add(Function);
            Atom Character = new Atom("ID05", "^'[a-zA-Z]'$", @"^'([a-zA-Z]'?)?$");
            Character.IsReservedWord = false;
            Atoms.Add(Character);
            Atom FloatNumber = new Atom("ID06", @"^[0-9]+\.[0-9]+(e(-|\+)?[0-9]+)?$", @"^[0-9]+(\.([0-9]+((e((-|\+)?[0-9]*)?)?)?)?)?$");
            FloatNumber.IsReservedWord = false;
            Atoms.Add(FloatNumber);
        }

        private void AddCodeTypes()
        {
            DefaultCodeTypes.Add("ID01", "VOI");
            DefaultCodeTypes.Add("ID02", "STR");
            DefaultCodeTypes.Add("ID03", "INT");
            DefaultCodeTypes.Add("ID04", "VOID");
            DefaultCodeTypes.Add("ID05", "CHC");
            DefaultCodeTypes.Add("ID06", "PFO");

            //          PFO(ponto flutuante) INT(inteiro), STR(string), CHC
            //          (character), BOO(booleano), VOI(void), APF(array de ponto flutuante)
            //          AIN(array de inteiro), AST(array de string), ACH(array de character), 
            //          ABO(array de booleano
        }

        public bool HasType(string code)
        {
            bool KeyExistence = DefaultCodeTypes.ContainsKey(code);

            return KeyExistence;   
        }

        public string GetType(string code)
        {
            string Type = DefaultCodeTypes[code];            

            return Type;
        }

    }
}