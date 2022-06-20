using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerDK
{
    internal class LexicalAnalyzer
    {
        public LanguageSymbolTable LanguageSymbolTable { get; set; }
        public LanguageSymbolTable CompilationSymbolTable { get; set; }

        public LexicalAnalyzer(LanguageSymbolTable languageSymbolTable)
        {
            LanguageSymbolTable = languageSymbolTable;
        }

        public Atom IdenfifyAtom(string source, int startPosition) //
        {
            char character;
            string lexeme = "";
            int position = startPosition;
            List<Atom> passList = new List<Atom>(LanguageSymbolTable.Atoms);

            do
            {   //loop para formar o maior lexeme possível
                character = source[position];
                lexeme += character;
                passList = PossibleAtoms(lexeme, passList);
                position++;

                // assim que o lexeme não pode mais formar um átomo ele já está em seu maior tamanho
            } while (passList.Count > 0); 

            passList = new List<Atom>(LanguageSymbolTable.Atoms);
            // Em seguida reduzimos o lexeme até que possa ser um átomo novamente
            do
            {
                if(lexeme.Length > 1)
                    lexeme = lexeme.Remove(lexeme.Length - 1); //vai removendo até formar alguma coisa
                    position--;// aqui retrata a posição que estamos no arquivo
                passList = PossibleAtoms(lexeme, passList);

            } while (passList.Count == 0 && lexeme.Length > 1) ;
            // Verificação que garante que um lexeme é um átomo específico

            //aqui eu vou colocar o átomo na tabela e retornar a posição final
            return FinalAtom(lexeme, passList);
        }

        public List<Atom> PossibleAtoms(string lexeme, List<Atom> passList) // ver se passo aqui a passlist
        {
            foreach (Atom a in LanguageSymbolTable.Atoms)
            {
                bool canBe = a.PartialValidation(lexeme);
                if (!canBe)
                {
                    passList.Remove(a); // remove o atual da lista
                };
            };
            return passList;
        }

        public Atom FinalAtom(string lexeme, List<Atom> passList)
        {
            foreach (Atom a in passList)
            {
                bool canBe = a.FinalValidation(lexeme);
                if (!canBe)
                {
                    passList.Remove(a);
                };

            };

            if(passList.Count == 1)
            {
                return passList[0];
            }
            else
            {
                return null;
            }
        }

        // Implementar truncagem
        // Implementar filtragem de caracteres
        // Implementar controle de linhas -> Em que linha está o caractér localizado na "startPosition"
    }
}
