using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.tools.lexer.tokenizer
{
    class Token
    {
        private string value;
        private Lexem lexemType;

        public Token(string value, Lexem lexemType)
        {
            this.value = value;
            this.lexemType = lexemType;
        }

        public string getValue()
        {
            return this.value;
        }

        public Lexem getLexemType()
        {
            return this.lexemType;
        }

        public void setValue(string value)
        {
            this.value = value;
        }

        public void setLexemType(Lexem lexemType)
        {
            this.lexemType = lexemType;
        }
    }
}
