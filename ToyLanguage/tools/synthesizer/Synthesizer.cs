using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.tools.lexer;
using ToyLanguage.tools.lexer.tokenizer;
using ToyLanguage.tools.parser;

namespace ToyLanguage.tools.synthesizer
{
    class Synthesizer
    {
        public static List<Token> tokens;
        public static List<Token> outString = new List<Token>();
        public static Stack<Token> tokenStack = new Stack<Token>();
        public static LexemTypeEnum lexemTypeEnum = new LexemTypeEnum();
        public static TerminalTypeEnum terminalTypeEnum = new TerminalTypeEnum();
        public static int pointer = 0;

        public Synthesizer() { }

        public List<Token> start(List<Token> tokensInput)
        {
            tokens = tokensInput;
            read();
            outString.Add(new Token("$", terminalTypeEnum.types["END"]));
            return outString;
        }

        private void read()
        {
            bool reading = true;
            while (reading) 
            {
                reading = expression();
            }
        }

        private bool expression()
        {
            Lexem temp = tokens[pointer].getLexemType();
            switch (temp.getType())
            {
                case "DIGIT":
                case "VAR":
                    outString.Add(tokens[pointer]);
                    pointer++;
                    break;
                case "OP":
                case "ASSIGN_OP":
                case "COMPARISON_OP":
                    while (tokenStack.Count != 0 && getPriority(tokens[pointer], tokenStack.Peek()))
                        outString.Add(tokenStack.Pop());
                    tokenStack.Push(tokens[pointer]);
                    pointer++;
                    break;
                case "LP":
                    tokenStack.Push(tokens[pointer]);
                    pointer++;
                    break;
                case "RP":
                    while (tokenStack.Count != 0 && tokenStack.Peek().getLexemType().getType() != lexemTypeEnum.types["LP"].getType())
                        outString.Add(tokenStack.Pop());
                    tokenStack.Pop();
                    pointer++;
                    break;
                case "IF":
                    conditionalExpression();
                    int tempIfPosition = outString.Count;
                    outString.Add(new Token("", terminalTypeEnum.types["GOTO_POINT"]));
                    outString.Add(new Token("!F", terminalTypeEnum.types["GOTO_ON_FALSE"]));
                    body();
                    if (tokens[pointer].getLexemType().getType() == lexemTypeEnum.types["ELSE"].getType())
                    {
                        pointer++;
                        int tempElsePosition = outString.Count;
                        outString.Add(new Token("", terminalTypeEnum.types["GOTO_POINT"]));
                        outString.Add(new Token("!", terminalTypeEnum.types["GOTO"]));
                        outString[tempIfPosition].setValue(outString.Count.ToString());
                        body();
                        outString[tempElsePosition].setValue(outString.Count.ToString());
                    }
                    else
                    {
                        outString[tempIfPosition].setValue(outString.Count.ToString());
                    }
                    break;
                case "WHILE":
                    int tempWhilePosition = outString.Count;
                    conditionalExpression();
                    int tempEndPosition = outString.Count;
                    outString.Add(new Token("", terminalTypeEnum.types["GOTO_POINT"]));
                    outString.Add(new Token("!F", terminalTypeEnum.types["GOTO_ON_FALSE"]));
                    body();
                    outString.Add(new Token(tempWhilePosition.ToString(), terminalTypeEnum.types["GOTO_POINT"]));
                    outString.Add(new Token("!", terminalTypeEnum.types["GOTO"]));
                    outString[tempEndPosition].setValue(outString.Count.ToString());
                    break;
                case "PRINT":
                    pointer++;
                    tokenStack.Push(tokens[pointer]);
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["EOE"].getType())
                        expression();
                    outString.Add(new Token("print", lexemTypeEnum.types["PRINT"]));
                    pointer++;
                    break;
                case "LIST":
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["EOE"].getType())
                        expression();
                    pointer++;
                    outString.Add(new Token("list", lexemTypeEnum.types["LIST"]));
                    break;
                case "HASHTABLE":
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["EOE"].getType())
                        expression();
                    pointer++;
                    outString.Add(new Token("hashtable", lexemTypeEnum.types["HASHTABLE"]));
                    break;
                case "INSERT":
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["INTO"].getType())
                        expression();
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["ON"].getType())
                        expression();
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["EOE"].getType())
                        expression();
                    pointer++;
                    outString.Add(new Token("insert", lexemTypeEnum.types["INSERT"]));
                    break;
                case "DELETE":
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["FROM"].getType())
                        expression();
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["EOE"].getType())
                        expression();
                    pointer++;
                    outString.Add(new Token("delete", lexemTypeEnum.types["DELETE"]));
                    break;
                case "GET":
                    pointer++;
                    while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["FROM"].getType())
                        expression();
                    pointer++;
                    expression();
                    pointer++;
                    outString.Add(new Token("get", lexemTypeEnum.types["GET"]));
                    while (tokenStack.Count != 0 && getPriority(tokens[pointer], tokenStack.Peek()))
                        outString.Add(tokenStack.Pop());
                    break;
                case "COUNTOF":
                    pointer++;
                    expression();
                    pointer++;
                    outString.Add(new Token("countof", lexemTypeEnum.types["COUNTOF"]));
                    while (tokenStack.Count != 0 && getPriority(tokens[pointer], tokenStack.Peek()))
                        outString.Add(tokenStack.Pop());
                    break;
                case "EOE":
                    while (tokenStack.Count != 0)
                        outString.Add(tokenStack.Pop());
                    pointer++;
                    break;
                case "END":
                    while (tokenStack.Count != 0)
                        outString.Add(tokenStack.Pop());
                    return false;
                default:
                    return false;
            }
            return true;
        }

        private void conditionalExpression()
        {
            pointer++;
            while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["LB"].getType())
                expression();
        }

        private void body()
        {
            pointer++;
            while (tokens[pointer].getLexemType().getType() != lexemTypeEnum.types["RB"].getType())
                expression();
            while (tokenStack.Count != 0)
                outString.Add(tokenStack.Pop());
            pointer++;
        }

        private bool getPriority(Token frstToken, Token scndToken)
        {
            return getPriorityNumber(frstToken) <= getPriorityNumber(scndToken);
        }

        private int getPriorityNumber(Token token)
        {
            switch (token.getValue())
            {
                case "=":
                case ">":
                case "<":
                case ">=":
                case "<=":
                case "!=":
                case "==":
                    return 1;
                case "+":
                case "-":
                    return 2;
                case "/":
                    return 4;
                case "*":
                    return 5;
                default:
                    return -1;
            }
        }
    }
}
