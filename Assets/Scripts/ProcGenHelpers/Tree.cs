using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProcGenHelpers
{
    class Tree<T>
    {
        public T value;
        public Tree<T> left;
        public Tree<T> right;

        public Tree(T val, Tree<T> l = null, Tree<T> r = null)
        {
            value = val;
            left = l;
            right = r;
        }


    }
}
