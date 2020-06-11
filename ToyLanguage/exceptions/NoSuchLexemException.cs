using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.exceptions
{
    class NoSuchLexemException : Exception
    {
        private string value;

        public NoSuchLexemException(string value)
        {
            this.value = value;
        }

        public string getValue()
        {
            return value;
        }
    }
}
