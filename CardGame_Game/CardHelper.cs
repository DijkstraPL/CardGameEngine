using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Game
{
    public static class CardHelper
    {
        public static Stack<T> Shuffle<T>(this Stack<T> stack)
        {
            var random = new Random();
            var values = stack.ToArray();
            stack.Clear();
            foreach (var value in values.OrderBy(x => random.Next()))
                stack.Push(value);
            return stack;
        }
    }
}
