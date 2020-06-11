using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.tools.lexer
{
    class Lexem
    {
        private string matchExpr;
        private string type;

        public Lexem(string matchExpr, string type)
        {
            this.matchExpr = matchExpr;
            this.type = type;
        }

        public string getMatchExpr()
        {
            return this.matchExpr;
        }

        public string getType()
        {
            return this.type;
        }
    }
}
