using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var path = @"E:\davim\GitHub\DKSCompiler\CompilerDK\teste.txt";
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }
}