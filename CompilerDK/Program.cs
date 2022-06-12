using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = FileReader();
        CreateBruteAutomate(lines, 116);
    }

    private static string[] FileReader()
    {
        string[] lines = { "" };

        Console.WriteLine(" Enter the path to te .dks format file: ");
        string path = Console.ReadLine();

        //Exemplo de arquivo para facilitar os testes: E:\davim\GitHub\DKSCompiler\CompilerDK\wirth.dks

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

    private static void CreateBruteAutomate(string[] lines, int j)
    {
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
                        Console.Write(number + j + " ");
                        numberStr = "";
                    }
                } else
                {
                    Console.Write(ch);
                }
            }

            Console.Write('\n');
        }
    }
}
