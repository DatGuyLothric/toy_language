using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyLanguage.types.doubly_linked_list
{
    class DoublyLinkedList
    {
        private int count = 0;
        private DoublyLinkedListNode head;
        private DoublyLinkedListNode tail;

        public int Count()
        {
            return this.count;
        }

        public void Insertion(double value)
        {
            DoublyLinkedListNode newNode = new DoublyLinkedListNode(value);
            if (this.head == null)
                this.head = this.tail = newNode;
            if (this.head != null)
            {
                newNode.setNext(this.head);
                this.head = newNode;
                newNode.getNext().setPrev(this.head);
            }
            this.count++;
        }

        public void Deletion()
        {
            DoublyLinkedListNode temp = this.head;
            if (this.head.getNext() != null)
                this.head.getNext().setPrev(null);
            this.head = this.head.getNext();
            this.count--;
        }

        public void InsertLast(double value)
        {
            DoublyLinkedListNode newNode = new DoublyLinkedListNode(value);
            if (this.head == null)
                this.head = this.tail = newNode;
            if (this.head != null)
            {
                this.tail.setNext(newNode);
                newNode.setPrev(this.tail);
                this.tail = newNode;
            }
            this.count++;
        }

        public void DeleteLast()
        {
            DoublyLinkedListNode temp = this.tail;
            if (this.tail.getPrev() != null)
                this.tail.getPrev().setNext(null);
            this.tail = this.tail.getPrev();
            this.count--;
        }

        public void InsertAfter(double value, int index)
        {
            if (index == 0)
                this.Insertion(value);
            if (index == this.count)
                this.InsertLast(value);
            if (index != 0 && index != this.count)
            {
                int pointer = 0;
                DoublyLinkedListNode temp = this.head;
                DoublyLinkedListNode newNode = new DoublyLinkedListNode(value);
                while (pointer != index)
                {
                    temp = temp.getNext();
                    pointer++;
                }
                temp.getPrev().setNext(newNode);
                newNode.setPrev(temp.getPrev());
                temp.setPrev(newNode);
                newNode.setNext(temp);
                this.count++;
            }
        }

        public void Delete(int index)
        {
            if (index >= this.count)
                throw new Exception("Exception: there is no such element in linked list");
            if (index == 0)
                this.Deletion();
            if (index == this.count - 1)
                this.DeleteLast();
            if (index != 0 && index != this.count - 1)
            {
                int pointer = 0;
                DoublyLinkedListNode temp = this.head;
                while (pointer != index)
                {
                    temp = temp.getNext();
                    pointer++;
                }
                temp.getPrev().setNext(temp.getNext());
                temp.getNext().setPrev(temp.getPrev());
                this.count--;
            }
        }

        public int Search(double value)
        {
            DoublyLinkedListNode temp = this.head;
            int pointer = 0;
            while (temp.getValue() != value)
            {
                temp = temp.getNext();
                pointer++;
            }
            return pointer;
        }

        public double Return(int index)
        {
            if (index >= this.count)
                throw new Exception("Exception: there is no such element in linked list");
            DoublyLinkedListNode temp = this.head;
            int pointer = 0;
            while (pointer != index)
            {
                temp = temp.getNext();
                pointer++;
            }
            return temp.getValue();
        }
    }
}
