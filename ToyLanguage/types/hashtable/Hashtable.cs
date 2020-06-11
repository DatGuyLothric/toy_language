using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.types.hashtable
{
    class Hashtable
    {
        private static readonly int SIZE = 32;
        HashtableDataItem[] table = new HashtableDataItem[SIZE];

        public Hashtable() 
        {
            for (int i = 0; i < SIZE; i++)
                table[i] = null;
        }   

        public double Search(int key)
        {
            int hash = hashFunc(key);
            HashtableDataItem head = table[hash];
            while (head != null)
            {
                if (head.getKey().Equals(key))
                    return head.getValue();
                head = head.next;
            }
            throw new Exception("Exception: there is no such element in hashtable");
        }

        public void Insert(double value, int key)
        {
            int hash = hashFunc(key);
            HashtableDataItem head = table[hash];
            while (head != null)
            {
                if (head.getKey().Equals(key))
                {
                    head.setValue(value);
                    return;
                }
                head = head.next;
            }
            head = table[hash];
            HashtableDataItem newItem = new HashtableDataItem(value, key, head);
            table[hash] = newItem;
        }

        public void Delete(int key)
        {
            int hash = hashFunc(key);
            HashtableDataItem head = table[hash];
            HashtableDataItem prev = null;
            while (head != null)
            {
                if (head.getKey().Equals(key))
                    break;
                prev = head;
                head = head.next;
            }
            if (head == null)
                throw new Exception("Exception: there is no such element in hashtable");
            if (prev != null)
                prev.next = head.next;
            else
                table[hash] = head.next;
        }

        private int hashFunc(int key)
        {
            return key % SIZE;
        }
    }
}
