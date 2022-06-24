﻿using System;
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
        public List<Atom> CurrentPassList { get; set; }

        public LexicalAnalyzer(LanguageSymbolTable languageSymbolTable)
        {
            LanguageSymbolTable = languageSymbolTable;
        }

        public Atom IdenfifyAtom(string source, int startPosition, bool isFunction) //
        {
            string lexeme = "";
            Atom finalAtom = null;
            CurrentPosition = startPosition;
            CurrentPassList = new List<Atom>(LanguageSymbolTable.Atoms);

            lexeme = GenerateLargestLexeme(source);


            // Se o lexeme é o último do source verificamos se já forma um átomo
            CurrentPassList = PossibleAtoms(lexeme);
            if(CurrentPassList.Count == 0) //se não for nenhum átomo, reduzimos até virar um
            {
                CurrentPassList = new List<Atom>(LanguageSymbolTable.Atoms);
                // Em seguida reduzimos o lexeme até que possa ser um átomo novamente
                lexeme = ReduceLexeme(lexeme);

            }
            lexeme = Truncate(lexeme);
            //aqui eu vou colocar o átomo na tabela e retornar a posição final
            finalAtom = FinalAtom(lexeme, isFunction);
            return finalAtom;

        }

        public string Truncate(string lexeme)
        {
            if(lexeme.Length > 35)
            {
                string truncatedLexeme = lexeme.Substring(0, 35);

                CurrentPassList = new List<Atom>(LanguageSymbolTable.Atoms);
                CurrentPassList = PossibleAtoms(truncatedLexeme);
                if (CurrentPassList.Count == 0) //se não for nenhum átomo, reduzimos até virar um
                {
                    CurrentPassList = new List<Atom>(LanguageSymbolTable.Atoms);
                    // Em seguida reduzimos o lexeme até que possa ser um átomo novamente
                    lexeme = ReduceLexeme(truncatedLexeme);
                    //adicionar o fecha aspas da string
                }
                return truncatedLexeme;
            }
            return lexeme;
        }

        public string GenerateLargestLexeme(string source)
        {
            string character;
            string lexeme = "";
            if (source.Length == 0)
            {
                return "";
            }

            do
            {   //loop para formar o maior lexeme possível
                character = source[CurrentPosition].ToString();
                if (LanguageSymbolTable.LanguageCharacterValidator.IsMatch(character))
                {
                    lexeme += character;
                    CurrentPassList = PossibleAtoms(lexeme);
                }
                CurrentPosition++;

                // assim que o lexeme não pode mais formar um átomo ele já está em seu maior tamanho
            } while (CurrentPassList.Count > 0 && CurrentPosition < source.Length);
            return lexeme;
        }

        public string ReduceLexeme(string lexeme)
        {
            do
            {
                if (lexeme.Length > 1)
                {
                    lexeme = lexeme.Remove(lexeme.Length - 1); //vai removendo até formar alguma coisa
                    CurrentPosition--;// aqui retrata a posição que estamos no arquivo
                }

                CurrentPassList = PossibleAtoms(lexeme);

            } while (CurrentPassList.Count == 0 && lexeme.Length > 1);
            // Verificação que garante que um lexeme é um átomo específico

            return lexeme;
        }

        public List<Atom> PossibleAtoms(string lexeme) // ver se passo aqui a passlist
        {
            List<Atom> possibleAtoms = new List<Atom>(CurrentPassList);
            foreach (Atom a in CurrentPassList)
            {
                bool canBe = a.PartialValidation(lexeme);
                if (!canBe)
                {
                    possibleAtoms.Remove(a); // remove o atual da lista
                };
            };
            return possibleAtoms;
        }

        public Atom FinalAtom(string lexeme, bool isFunction)
        {
            List<Atom> finalList = new List<Atom>(CurrentPassList);
            foreach (Atom a in CurrentPassList)
            {
                bool canBe = a.FinalValidation(lexeme);
                //se for uma função remove o identifier
                if (!canBe || (isFunction && a.Code == "ID01") || (!isFunction && a.Code == "ID04"))
                {
                    finalList.Remove(a);
                };

            };

            if(finalList.Count == 1)
            {
                return finalList[0];
            }
            else if (finalList.Count > 1)
            {
                Atom finalAtom = finalList.Where(atom => atom.IsReservedWord == true).Last();
                return  finalAtom != null ? finalAtom : null;
            }
            else
            {
                return null;
            }
        }

    }
}
