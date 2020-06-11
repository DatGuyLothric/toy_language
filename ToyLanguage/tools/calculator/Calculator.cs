using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.types.hashtable;
using ToyLanguage.exceptions;
using ToyLanguage.tools.lexer;
using ToyLanguage.tools.lexer.tokenizer;
using ToyLanguage.tools.parser;
using ToyLanguage.types.doubly_linked_list;

namespace ToyLanguage.tools.calculator
{
    class Calculator
    {
        public static Dictionary<string, object> variables = new Dictionary<string, object>();
        public static Dictionary<string, DoublyLinkedList> lists = new Dictionary<string, DoublyLinkedList>();
        public static Dictionary<string, types.hashtable.Hashtable> hashtables = new Dictionary<string, types.hashtable.Hashtable>();
        public static Stack<object> calcStack = new Stack<object>();
        public static int pointer = 0;
        public static LexemTypeEnum lexemTypeEnum = new LexemTypeEnum();
        public static TerminalTypeEnum terminalTypeEnum = new TerminalTypeEnum();

        public Calculator() { }

        public void start(List<Token> postfixNotation)
        {
            while (postfixNotation[pointer].getLexemType().getType() != terminalTypeEnum.types["END"].getType())
            {
                Lexem temp = postfixNotation[pointer].getLexemType();
                double temp1 = 0;
                double temp2 = 0;
                switch(temp.getType())
                {
                    case "VAR":
                        calcStack.Push(postfixNotation[pointer].getValue());
                        break;
                    case "DIGIT":
                        calcStack.Push(Convert.ToDouble(postfixNotation[pointer].getValue()));
                        break;
                    case "PRINT":
                        if (variables.ContainsKey((string)calcStack.Peek()))
                        {
                            Console.WriteLine(variables[(string)calcStack.Peek()]);
                        }
                        else
                        {
                            throw new NoSuchVariableException((string)calcStack.Peek());
                        }
                        break;
                    case "LIST":
                        lists.Add((string)calcStack.Pop(), new DoublyLinkedList());
                        break;
                    case "HASHTABLE":
                        hashtables.Add((string)calcStack.Pop(), new types.hashtable.Hashtable());
                        break;
                    case "INSERT":
                        int pos = (int)(double)calcStack.Pop();
                        string name = (string)calcStack.Pop();
                        double value = (double)calcStack.Pop();
                        if (lists.ContainsKey(name))
                        {
                            lists[name].InsertAfter(value, pos);
                        }
                        else if (hashtables.ContainsKey(name))
                        {
                            hashtables[name].Insert(value, pos);
                        }
                        break;
                    case "DELETE":
                        name = (string)calcStack.Pop();
                        pos = (int)(double)calcStack.Pop();
                        if (lists.ContainsKey(name))
                        {
                            lists[name].Delete(pos);
                        }
                        else if (hashtables.ContainsKey(name))
                        {
                            hashtables[name].Delete(pos);
                        }
                        break;
                    case "COUNTOF":
                        name = (string)calcStack.Pop();
                        calcStack.Push((int)lists[name].Count());
                        break;
                    case "GET":
                        name = (string)calcStack.Pop();
                        pos = (int)(double)calcStack.Pop();
                        if (lists.ContainsKey(name))
                        {
                            calcStack.Push((double)lists[name].Return(pos));
                        }
                        else if (hashtables.ContainsKey(name))
                        {
                            calcStack.Push((double)hashtables[name].Search(pos));
                        }
                        break;
                    case "OP":
                        temp1 = get();
                        temp2 = get();
                        double aResult = 0;
                        switch (postfixNotation[pointer].getValue())
                        {
                            case "+":
                                aResult = temp2 + temp1;
                                break;
                            case "-":
                                aResult = temp2 - temp1;
                                break;
                            case "*":
                                aResult = temp2 * temp1;
                                break;
                            case "/":
                                aResult = temp2 / temp1;
                                break;
                            default:
                                break;
                        }
                        calcStack.Push(aResult);
                        break;
                    case "ASSIGN_OP":
                        object var = calcStack.Pop();
                        try
                        {
                            temp1 = (double)var;
                            variables[(string)calcStack.Pop()] = temp1;
                        }
                        catch (Exception e)
                        {
                            int temp3 = (int)var;
                            variables[(string)calcStack.Pop()] = temp3;
                        }
                        
                        break;
                    case "COMPARISON_OP":
                        temp1 = get();
                        temp2 = get();
                        bool lResult = false;
                        switch (postfixNotation[pointer].getValue())
                        {
                            case "==":
                                lResult = temp2 == temp1;
                                break;
                            case "!=":
                                lResult = temp2 != temp1;
                                break;
                            case ">":
                                lResult = temp2 > temp1;
                                break;
                            case "<":
                                lResult = temp2 < temp1;
                                break;
                            case ">=":
                                lResult = temp2 >= temp1;
                                break;
                            case "<=":
                                lResult = temp2 <= temp1;
                                break;
                            default:
                                break;
                        }
                        calcStack.Push(lResult);
                        break;
                    case "GOTO_POINT":
                        calcStack.Push(Convert.ToInt32(postfixNotation[pointer].getValue()));
                        break;
                    case "GOTO_ON_FALSE":
                        temp1 = (int)calcStack.Pop();
                        bool condition = (bool)calcStack.Pop();
                        if (!condition)
                        {
                            pointer = (int)temp1;
                            pointer--;
                        }
                        break;
                    case "GOTO":
                        temp1 = (int)calcStack.Pop();
                        pointer = (int)temp1;
                        pointer--;
                        break;
                    default:
                        break;
                }
                pointer++;
            }
        }

        private double get()
        {
            if (calcStack.Peek() is string)
            {
                if (variables.ContainsKey((string)calcStack.Peek()) && (variables[(string)calcStack.Peek()] is double))
                    return (double)variables[(string)calcStack.Pop()];
                throw new NoSuchVariableException((string)calcStack.Peek());
            }
            if (calcStack.Peek() is double)
                return (double)calcStack.Pop();
            return -1;
        }
    }
}
