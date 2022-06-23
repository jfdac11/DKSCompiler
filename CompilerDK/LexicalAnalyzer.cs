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

        public int CurrentPosition { get; set; }

        public LexicalAnalyzer(LanguageSymbolTable languageSymbolTable)
        {
            LanguageSymbolTable = languageSymbolTable;
        }

        public Atom IdenfifyAtom(string source, int startPosition) //
        {
            string character;
            string lexeme = "";
            Atom finalAtom = null;
            int position = startPosition;
            List<Atom> passList = new List<Atom>(LanguageSymbolTable.Atoms);

            do
            {   //loop para formar o maior lexeme possível
                character = source[position].ToString();
                if (LanguageSymbolTable.LanguageCharacterValidator.IsMatch(character))
                {
                    lexeme += character;
                    passList = PossibleAtoms(lexeme, passList);
                }
                position++;

                // assim que o lexeme não pode mais formar um átomo ele já está em seu maior tamanho
            } while (passList.Count > 0 && position < source.Length);


            // Se o lexeme é o último do source verificamos se já forma um átomo
            passList = PossibleAtoms(lexeme, passList);
            if(passList.Count > 0)
            {
                finalAtom = FinalAtom(lexeme, passList);
                CurrentPosition = position-1;
                return finalAtom;
            }

            passList = new List<Atom>(LanguageSymbolTable.Atoms);
            // Em seguida reduzimos o lexeme até que possa ser um átomo novamente
            do
            {
                
                if (lexeme.Length > 1)
                {
                    lexeme = lexeme.Remove(lexeme.Length - 1); //vai removendo até formar alguma coisa
                    position--;// aqui retrata a posição que estamos no arquivo
                }
                    
                passList = PossibleAtoms(lexeme, passList);

            } while (passList.Count == 0 && lexeme.Length > 1 && position < source.Length) ;
            // Verificação que garante que um lexeme é um átomo específico

            //aqui eu vou colocar o átomo na tabela e retornar a posição final
            finalAtom = FinalAtom(lexeme, passList);
            CurrentPosition = position;
            return finalAtom;
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
            List<Atom> finalList = new List<Atom>(passList);
            foreach (Atom a in passList)
            {
                bool canBe = a.FinalValidation(lexeme);
                if (!canBe)
                {
                    finalList.Remove(a);
                };

            };

            // aqui, de acordo com o escopo (a lista vai ter tamanho > 1)
            // vai verificar se é um identificador ou uma função

            if(finalList.Count == 1)
            {
                return finalList[0];
            }
            else
            {
                return null;
            }
        }

        // Implementar truncagem
        // Implementar filtragem de caracteres (ok)
        // Implementar controle de linhas -> Em que linha está o caractér localizado na "startPosition"
    }
}
