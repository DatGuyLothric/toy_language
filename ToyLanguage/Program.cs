using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "";
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\example.txt");
            while (!reader.EndOfStream)
            {
                inputString += reader.ReadLine() + " ";
            }
            Regex reg = new Regex(@"list");
            inputString = reg.Replace(inputString, "_list_");
            reg = new Regex(@"hashtable");
            inputString = reg.Replace(inputString, "_hashtable_");
            reg = new Regex(@"get");
            inputString = reg.Replace(inputString, "_get_");
            reg = new Regex(@"countof");
            inputString = reg.Replace(inputString, "_countof_");
            reg = new Regex(@"into");
            inputString = reg.Replace(inputString, "_into_");
            reg = new Regex(@"from");
            inputString = reg.Replace(inputString, "_from_");
            reg = new Regex(@"insert");
            inputString = reg.Replace(inputString, "_insert_");
            reg = new Regex(@"delete");
            inputString = reg.Replace(inputString, "_delete_");
            reg = new Regex(@"on");
            inputString = reg.Replace(inputString, "_on_");
            Regex sWhitespace = new Regex(@"\s+");
            inputString = sWhitespace.Replace(inputString, "");
            Console.WriteLine(inputString);
            Interpreter.start(inputString);
        }
    }
}
