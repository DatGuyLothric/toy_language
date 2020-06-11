using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.tools.lexer;
using ToyLanguage.tools.lexer.tokenizer;

namespace ToyLanguage.tools.parser
{
    struct SyntaxErrorStruct
    {
        public int position;
        public Token token;
        public Lexem lexem;

        public SyntaxErrorStruct(int position, Token token, Lexem lexem)
        {
            this.position = position;
            this.token = token;
            this.lexem = lexem;
        }
    }

    class SyntaxError
    {
        private Stack<SyntaxErrorStruct> errors = new Stack<SyntaxErrorStruct>();

        public SyntaxError() { }

        public bool add(int position, Token token, Lexem lexem)
        {
            this.errors.Push(new SyntaxErrorStruct(position, token, lexem));
            return true;
        }

        public void printError()
        {
            Console.WriteLine("Syntax error on " + this.errors.Peek().position + ": Lexem " + this.errors.Peek().lexem.getType() + " expected, but " + this.errors.Peek().token.getLexemType().getType() + " found!");
        }
    }
}
