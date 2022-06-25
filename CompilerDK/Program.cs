using System.Text;
using CompilerDK;
using System.Configuration;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        LanguageSymbolTable languageSymbolTable = new LanguageSymbolTable();
        SymbolTable symbolTable = new SymbolTable();
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(languageSymbolTable);
        LexicalTableReport lexicalAnalysisReport = new LexicalTableReport();

        string filePath = "";
        string[] lines = { "" };
        bool error;

        //string filePath = @"E:\Projetos\Faculdade\DKSCompiler\CompilerDK\teste.dks";
        //@"D:\Users\maria\Documents\SENAI\7º semestre\Compiladores\DKSCompiler\CompilerDK\teste.dks";
        // @"E:\davim\GitHub\DKSCompiler\CompilerDK\teste.dks";

        do
        {
            try
            {
                error = false;
                filePath = GetFilePath();
                string extension = Path.GetExtension(filePath);
                lines = FileReader(filePath);
            }
            catch
            {
                error = true;
            }
        } while (error);


        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string directoryPath = Path.GetDirectoryName(filePath);


        bool isBlockComment = false;

        for (int i = 0; i < lines.Count(); i++)
        {
            string line = lines[i];
            int startPosition = 0;

            do
            {
                if (OpenBlockComment(line, startPosition))
                {
                    isBlockComment = true;
                    startPosition ++;
                }
                else if (ClosesBlockComment(line, startPosition))
                {
                    isBlockComment = false;
                    startPosition += 2;
                }

                if (!isBlockComment)
                {
                    if (IsLineComment(line, startPosition))
                    {
                        startPosition = line.Length;
                    }

                    if (startPosition < line.Length)
                    {
                        Symbol symbolResp = lexicalAnalyzer.IdenfifyAtom(line, startPosition); //átomo encontrado

                        if (symbolResp.Atom != null)
                        {
                            if (symbolResp.Atom.Code == "SR03")
                            {
                                if (symbolTable.Symbols.Last().Atom.Code == "*")
                                {
                                    Atom Function = new Atom("ID04", "^([a-zA-Z]+[0-9]*)+$", "^([a-zA-Z]+[0-9]*)+$");
                                    Function.IsReservedWord = false;
                                    symbolTable.Symbols.Last().Atom = Function;

                                    lexicalAnalysisReport.FoundedAtoms.Last().AtomCode = Function.Code;
                                }
                            }

                            if (languageSymbolTable.HasType(symbolResp.Atom.Code))
                                symbolResp.Type = languageSymbolTable.GetType(symbolResp.Atom.Code);
                            else
                                symbolResp.Type = "-";

                            symbolResp.Lines.Add(i + 1);

                            int lastIndex = symbolTable.SearchSymbolIndex(symbolResp.Lexeme);

                            if (lastIndex == -1)
                                lastIndex = symbolTable.AddSymbolToTable(symbolResp);
                            else
                                symbolTable.UpdateSymbolTable(symbolResp);

                            LexicalItemTable itemTable = new LexicalItemTable(symbolResp.Lexeme, symbolResp.Atom.Code, lastIndex);

                            lexicalAnalysisReport.FoundedAtoms.Add(itemTable);
                        }
                        startPosition = lexicalAnalyzer.CurrentPosition;
                    }
                }
                else
                {
                    startPosition++;
                }

            } while (startPosition < line.Length);

        }

        symbolTable = AddIdentifierCode(symbolTable);
        lexicalAnalysisReport = AddIdentifierCode(lexicalAnalysisReport);

        symbolTable.GenerateSymbolTableReport(fileName, directoryPath);
        symbolTable.ShowSymbolTableItems(fileName);
        lexicalAnalysisReport.GenerateLexicalTableReport(fileName, directoryPath);
        lexicalAnalysisReport.ShowTableReport(fileName);


        // a partir da sequência de átomos criar uma função para definição de escopo
        // vai identificar a sequência de átomos

    }

    public static LexicalTableReport AddIdentifierCode(LexicalTableReport lexicalTableReport)
    {
        lexicalTableReport.FoundedAtoms.Where(sym => sym.AtomCode.Equals("*"))
                .ToList().ForEach(s => s.AtomCode = "ID01");
        return lexicalTableReport;
    }

    public static SymbolTable AddIdentifierCode(SymbolTable symbolTable)
    {
        Atom Identifier = new Atom("ID01", "^(([a-zA-Z]|_)+[0-9]*)+$", "^(([a-zA-Z]|_)+[0-9]*)+$");
        Identifier.IsReservedWord = false;

        symbolTable.Symbols.Where(sym => sym.Atom.Code.Equals("*"))
                .ToList().ForEach(s => s.Atom = Identifier);
        return symbolTable;
    }

    public static bool IsLineComment(string source, int position)
    {
        if (position >= source.Length - 1)
        {
            return false;
        }
        string character = source[position].ToString();
        string nextCharacter = source[position + 1].ToString();

        return (character == "/" && nextCharacter == "/");
    }

    public static bool OpenBlockComment(string source, int position)
    {
        if (position >= source.Length - 1)
        {
            return false;
        }
        string character = source[position].ToString();
        string nextCharacter = source[position + 1].ToString();

        return (character == "/" && nextCharacter == "*");
    }

    public static bool ClosesBlockComment(string source, int position)
    {
        if (position >= source.Length - 1)
        {
            return false;
        }
        string character = source[position].ToString();
        string nextCharacter = source[position + 1].ToString();

        return (character == "*" && nextCharacter == "/");
    }

    private static string GetFilePath()
    {
        string path;

        Console.WriteLine(" Enter the path to te .dks format file: ");
        path = Console.ReadLine();

        if (string.IsNullOrEmpty(path))
        {
            string err = "\nERRO: No file specified, please select a .dks file\n";
            Console.WriteLine(err);
            throw new Exception(err);
        }
        else if (!Path.GetExtension(path).Equals(".dks"))
        {
            if (Path.GetExtension(path).Equals(""))
            {
                path += ".dks";
            }
            else
            {
                string err = "\nERRO: Invalid file format, please select a .dks extension file\n";
                Console.WriteLine(err);
                throw new Exception(err);
            }
        }

        return path;
    }

    private static string[] FileReader(string filePath)
    {
        string[] lines;

        try
        {
            lines = File.ReadAllLines(filePath, Encoding.UTF8);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("\nERRO: The file cannot be found.\n");
            throw;
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("\nERRO: The directory cannot be found.\n");
            throw;
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("\nERRO: 'path' exceeds the maxium supported path length.\n");
            throw;
        }
        catch (Exception err)
        {
            Console.WriteLine("\nERRO: Ocorreu um erro desconhecido", err);
            throw;
        }
        return lines;
    }

}
