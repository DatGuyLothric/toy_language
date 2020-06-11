using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.tools.lexer
{
    class LexemTypeEnum
    {
        public Dictionary<string, Lexem> types;

        public LexemTypeEnum()
        {
            this.types = new Dictionary<string, Lexem>();
            this.types.Add("VAR", new Lexem(@"^([A-z]+)$", "VAR"));
            this.types.Add("DIGIT", new Lexem(@"^(0|[1-9][0-9]*)$", "DIGIT"));
            this.types.Add("RP", new Lexem(@"^\)$", "RP"));
            this.types.Add("LP", new Lexem(@"^\($", "LP"));
            this.types.Add("OP", new Lexem(@"^(\+|-|\*|\/)$", "OP"));
            this.types.Add("ASSIGN_OP", new Lexem(@"^=$", "ASSIGN_OP"));
            this.types.Add("COMPARISON_OP", new Lexem(@"^(==|!=|>|<|>=|<=)$", "COMPARISON_OP"));
            this.types.Add("RB", new Lexem(@"^\}$", "RB"));
            this.types.Add("LB", new Lexem(@"^\{$", "LB"));
            this.types.Add("IF", new Lexem(@"^if$", "IF"));
            this.types.Add("ELSE", new Lexem(@"^else$", "ELSE"));
            this.types.Add("WHILE", new Lexem(@"^while$", "WHILE"));
            this.types.Add("PRINT", new Lexem(@"print$", "PRINT"));
            this.types.Add("EOE", new Lexem(@"^;$", "EOE"));
            this.types.Add("LIST", new Lexem(@"^list$", "LIST"));
            this.types.Add("HASHTABLE", new Lexem(@"^hashtable$", "HASHTABLE"));
            this.types.Add("INSERT", new Lexem(@"^insert$", "INSERT"));
            this.types.Add("INTO", new Lexem(@"^into$", "INTO"));
            this.types.Add("DELETE", new Lexem(@"^delete$", "DELETE"));
            this.types.Add("FROM", new Lexem(@"^from$", "FROM"));
            this.types.Add("GET", new Lexem(@"^get$", "GET"));
            this.types.Add("COUNTOF", new Lexem(@"^countof$", "COUNTOF"));
            this.types.Add("ON", new Lexem(@"^on$", "ON"));
        }
    }
}
