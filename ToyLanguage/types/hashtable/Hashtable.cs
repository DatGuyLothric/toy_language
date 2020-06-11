using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.types.hashtable
{
    class Hashtable
    {
        private readonly int SIZE = 32;
        private Dictionary<int, HashtableDataItem> items = new Dictionary<int, HashtableDataItem>(); 

        public Hashtable() { }

        public double Search(int key)
        {
            int hash = hashFunc(key);
            if (!this.items.ContainsKey(hash))
                throw new Exception("Exception: there is no such element in hashtable");
            return this.items[hash].getValue();
        }

        public void Insert(double value, int key)
        {
            int hash = hashFunc(key);
            HashtableDataItem newItem = new HashtableDataItem(value, key);
            if (!this.items.ContainsKey(hash))
                items.Remove(hash);
            items.Add(hash, newItem);
        }

        public void Delete(int key)
        {
            int hash = hashFunc(key);
            if (!this.items.ContainsKey(hash))
                throw new Exception("Exception: there is no such element in hashtable");
            items.Remove(hash);
        }

        private int hashFunc(int key)
        {
            return key % SIZE;
        }
    }
}
