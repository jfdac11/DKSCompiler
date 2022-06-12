using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        FileReader();
    }

  public static void FileReader()
    {
        Console.WriteLine(" Enter the path to te .dks format file: ");
        Console.Write("> ");
        string path = Console.ReadLine();

        //Exemplo de arquivo para facilitar os testes: E:\davim\GitHub\DKSCompiler\CompilerDK\wirth.dks

        if(string.IsNullOrEmpty(path))
            Console.WriteLine(" \nERRO: No file specified, please select a .dks file\n");
        else if (!Path.GetExtension(path).Equals(".dks"))
            Console.WriteLine(" \nERRO: Invalid file format, please select a .dks extension file\n");
        else
        {
            try
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    foreach (char character in line)
                        if (character == '.')
                        {

                        }
                }
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
            catch (Exception err) {
                Console.WriteLine("\nERRO: Ocorreu um erro desconhecido", err);
            }
        }
    }
}
