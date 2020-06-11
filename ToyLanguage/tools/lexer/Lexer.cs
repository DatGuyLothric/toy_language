using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToyLanguage.tools.lexer.tokenizer;
using ToyLanguage.exceptions;

namespace ToyLanguage.tools.lexer
{
    class Lexer
    {
        public Lexer() { }

        public List<Token> start(string inputString)
        {
            List<Token> tokens = new List<Token>();
            Stack<Token> possibleTokens = new Stack<Token>();
            LexemTypeEnum lexemTypeEnum = new LexemTypeEnum();
            string value = "";
            bool stopSearch = true;
            int range = 0;

            for (int i = 0; i < inputString.Length; i++)
            {
                if (!inputString[i].Equals('_'))
                    value += inputString[i];
                stopSearch = true;

                if (!inputString[i].Equals('_'))
                {
                    foreach (KeyValuePair<string, Lexem> lexemType in lexemTypeEnum.types)
                    {
                        if (Regex.IsMatch(value, lexemType.Value.getMatchExpr()))
                        {
                            possibleTokens.Push(new Token(value, lexemType.Value));
                            stopSearch = false;
                        }
                    }
                }

                if (stopSearch || i == inputString.Length - 1)
                {
                    if (possibleTokens.Count != 0)
                    {
                        tokens.Add(possibleTokens.Pop());
                        possibleTokens.Clear();
                        value = "";
                    }
                    else
                    {
                        range++;
                        if (range == 2)
                            throw new NoSuchLexemException(value);
                    }
                    if (!inputString[i].Equals('_'))
                        i = stopSearch && range != 2 ? i - 1 : i;
                }
            }

            return tokens;
        }
    }
}
