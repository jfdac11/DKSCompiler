using System.Text;
using CompilerDK;
using System.Configuration;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = FileReader();
        //CreateAutomateStates(lines);
        //CreateTransitionTable(lines);

        LanguageSymbolTable languageSymbolTable = new LanguageSymbolTable();
        LexicalAnalyzer l = new LexicalAnalyzer(languageSymbolTable);


        string text = "ifg ";
        Atom resp = l.IdenfifyAtom(text, 0);
        Console.WriteLine(resp.Code);
        

    }


    private static string[] FileReader()
    {
        string[] lines = { "" };

        //Console.WriteLine(" Enter the path to te .dks format file: ");
        //string path = Console.ReadLine();

        string path = @"E:\davim\GitHub\DKSCompiler\CompilerDK\wirth.dks";

        if (string.IsNullOrEmpty(path))
            Console.WriteLine(" \nERRO: No file specified, please select a .dks file\n");
        else if (!Path.GetExtension(path).Equals(".dks"))
            Console.WriteLine(" \nERRO: Invalid file format, please select a .dks extension file\n");
        else
        {
            try
            {
                lines = File.ReadAllLines(path, Encoding.UTF8);
                return lines;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nERRO: The file cannot be found.\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\nERRO: The directory cannot be found.\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("\nERRO: 'path' exceeds the maxium supported path length.\n");
            }
            catch (Exception err)
            {
                Console.WriteLine("\nERRO: Ocorreu um erro desconhecido", err);
            }
        }
        return lines;
    }

    private static void CreateAutomateStates(string[] lines)
    {
        Console.WriteLine("Enter your sum number: ");
        int sum = Int16.Parse(Console.ReadLine());
        Console.WriteLine("Enter your wrong number: ");
        int wrongNumber = Int16.Parse(Console.ReadLine());

        bool stateFound = false;
        string numberStr = "";
        int number = 0;

        foreach (string line in lines)
        {
            for (int charPos = 0; charPos < line.Length; charPos++)
            {
                char ch = line[charPos];

                if (ch == '.')
                {
                    stateFound = true;
                    Console.Write(ch);
                }
                else if (stateFound)
                {
                    bool isNumber = Char.IsNumber(ch);
                    if (isNumber)
                    {
                        numberStr += ch;
                    }
                    if (!isNumber || charPos + 1 == line.Length)
                    {
                        stateFound = false;
                        number = Int16.Parse(numberStr);
                        if (number >= wrongNumber)
                            Console.Write(number + sum + " ");
                        else
                            Console.Write(number + " ");
                        numberStr = "";
                    }
                }
                else
                {
                    Console.Write(ch);
                }
            }

            Console.Write('\n');
        }
    }

    private static void CreateTransitionTable(string[] lines)
    {
        bool stateFound = false;
        int statesInTransition = 0;
        string numberStr = "";
        int number = 0;

        foreach (string line in lines)
        {
            for (int charPos = 0; charPos < line.Length; charPos++)
            {
                char ch = line[charPos];

                if (ch == '.')
                {
                    if (statesInTransition == 0)
                        Console.Write('(');
                    else if (statesInTransition == 1)
                        Console.Write(") -> ");
                    stateFound = true;
                    //foundTransition = true;
                }
                else if (stateFound)
                {

                    bool isNumber = Char.IsNumber(ch);
                    if (isNumber)
                    {
                        numberStr += ch;
                    }
                    if (!isNumber || charPos + 1 == line.Length)
                    {

                        statesInTransition++;
                        stateFound = false;
                        number = Int16.Parse(numberStr);

                        numberStr = "";
                        if (statesInTransition == 2)
                        {
                            Console.Write(number + ")\n(" + number + ", ");
                            statesInTransition = 1;
                        }
                        else
                        {
                            Console.Write(number + ",");
                        }
                    }
                }
                else if (ch == '{' || ch == '}' || ch == '(' || ch == ')' || ch == '[' || ch == ']')
                {
                    Console.Write("VAZIO");
                }
                else if (ch != 32 && ch != 34)
                {
                    Console.Write(ch);
                } 
            }
        }
    }
}
