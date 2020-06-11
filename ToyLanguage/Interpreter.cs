using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.exceptions;
using ToyLanguage.tools.calculator;
using ToyLanguage.tools.lexer;
using ToyLanguage.tools.lexer.tokenizer;
using ToyLanguage.tools.parser;
using ToyLanguage.tools.synthesizer;

namespace ToyLanguage
{
    class Interpreter
    {
        public Interpreter() { }

        public static void start(string inputString)
        {
            List<Token> tokens = null;
            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            Synthesizer synthesizer = new Synthesizer();
            Calculator calculator = new Calculator();
            LexemTypeEnum lexemTypeEnum = new LexemTypeEnum();
            TerminalTypeEnum terminalTypeEnum = new TerminalTypeEnum();

            try
            {
                tokens = lexer.start(inputString);
                Console.WriteLine("\nTokens:");
                foreach (Token token in tokens)
                {
                    Console.WriteLine("{ " + token.getValue() + " : " + token.getLexemType().getType() + " }");
                }
            }
            catch (NoSuchLexemException e)
            {
                Console.WriteLine("NoSuchLexemException: ", e.getValue());
            }

            /*
            bool check = parser.start(tokens);
            if (check)
            {
                Console.WriteLine("\nNo syntax errors found!");
            }
            else
            {
                parser.error.printError();
                return;
            }
            */

            tokens.Add(new Token("$", terminalTypeEnum.types["END"]));
            List<Token> postfixNotation = synthesizer.start(tokens);
            Console.WriteLine();
            for (int i = 0; i < postfixNotation.Count; i++)
            {
                Console.Write(postfixNotation[i].getValue() + " ");
            }

            Console.WriteLine("\n\nProgram output:");
            try
            {
                calculator.start(postfixNotation);
            }
            catch (NoSuchVariableException e)
            {
                Console.WriteLine("NoSuchVariableException: " + e.getValue());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: ", e.Message);
            }

            Console.ReadKey();
        }
    }
}
