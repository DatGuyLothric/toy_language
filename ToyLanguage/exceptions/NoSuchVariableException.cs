using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.exceptions
{
    class NoSuchVariableException : Exception
    {
        private string value;

        public NoSuchVariableException(string value)
        {
            this.value = value;
        }

        public string getValue()
        {
            return value;
        }
    }
}
