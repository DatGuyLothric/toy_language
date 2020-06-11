using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.tools.lexer;

namespace ToyLanguage.tools.parser
{
    class TerminalTypeEnum
    {
        public Dictionary<string, Lexem> types;

        public TerminalTypeEnum()
        {
            this.types = new Dictionary<string, Lexem>();
            this.types.Add("END", new Lexem(@"$", "END"));
            this.types.Add("GOTO", new Lexem(@"!", "GOTO"));
            this.types.Add("GOTO_ON_FALSE", new Lexem(@"!F", "GOTO_ON_FALSE"));
            this.types.Add("GOTO_POINT", new Lexem(@"", "GOTO_POINT"));
        }
    }
}
