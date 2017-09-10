using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWebAPI
{
    public sealed class CommonStack
    {
        private static volatile CommonStack instance;
        private static object syncLock = new object();

        private CommonStack() { }

        public static CommonStack Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(syncLock)
                    {
                        if (instance == null) instance = new CommonStack();
                    }
                }
                return instance;
            }
        }

        private Stack<string> _stack = new Stack<string>();

        public void Push(string item) => _stack.Push(item);

        public string Peek() => _stack.Peek();

        public string Pop() => _stack.Pop();

        public int GetSize() => _stack.Count;
    }
}