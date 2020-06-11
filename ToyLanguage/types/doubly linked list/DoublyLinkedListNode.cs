using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.types.doubly_linked_list
{
    class DoublyLinkedListNode
    {
        private double value;
        private DoublyLinkedListNode prev;
        private DoublyLinkedListNode next;

        public DoublyLinkedListNode(double value)
        {
            this.value = value;
        }

        public void setValue(double value)
        {
            this.value = value;
            this.prev = null;
            this.next = null;
        }

        public double getValue()
        {
            return this.value;
        }

        public void setPrev(DoublyLinkedListNode prev)
        {
            this.prev = prev;
        }

        public DoublyLinkedListNode getPrev()
        {
            return this.prev;
        }

        public void setNext(DoublyLinkedListNode next)
        {
            this.next = next;
        }

        public DoublyLinkedListNode getNext()
        {
            return this.next;
        }
    }
}
