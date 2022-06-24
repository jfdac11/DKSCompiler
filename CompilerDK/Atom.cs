using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CompilerDK
{
    public class Atom
    {
        public string Code { get; set; }
        public string PartialPattern { get; set; }
        public string Pattern { get; set; }
        public bool IsReservedWord { get; set; } = true;

        public Atom(string code, string pattern, string partialPattern)
        {
            Code = code;
            Pattern = pattern;
            PartialPattern = partialPattern;
        }

        public bool PartialValidation(string lexeme)
        {
            Regex myRegexValidator = new Regex(this.PartialPattern, RegexOptions.IgnoreCase);
            return myRegexValidator.IsMatch(lexeme);
            
        }

        public bool FinalValidation(string lexeme)
        {
            Regex myRegexValidator = new Regex(this.Pattern, RegexOptions.IgnoreCase);
            return myRegexValidator.IsMatch(lexeme);
        }
    }

}
