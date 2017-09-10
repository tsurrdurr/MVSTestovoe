using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackWebAPI
{
    public class MyStack
    {
        public MyStack(string[] items)
        {
            foreach(var item in items)
            {
                _stack.Push(item);
            }
        }

        private Stack<string> _stack = new Stack<string>();

        public void Push(string item) => _stack.Push(item);

        public string Peek() => _stack.Peek();

        public string Pop() => _stack.Pop();

        public int GetSize() => _stack.Count;
    }
}