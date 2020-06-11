using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.types.hashtable
{
    class HashtableDataItem
    {
        private double value;
        private int key;
        public HashtableDataItem next;

        public HashtableDataItem(double value, int key, HashtableDataItem next)
        {
            this.value = value;
            this.key = key;
            this.next = next;
        }

        public int getKey()
        {
            return this.key;
        }

        public void setValue(double value)
        {
            this.value = value;
        }

        public double getValue()
        {
            return this.value;
        }
    }
}
