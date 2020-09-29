using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Helpers
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

        //public static int GetCost(this ICard card, CardColor cardColor)
        //{
        //    if (cardColor == CardColor.Blue)
        //        return card.CostBlue ?? 0;
        //    if (cardColor == CardColor.Red)
        //        return card.CostRed ?? 0;
        //    if (cardColor == CardColor.Green)
        //        return card.CostGreen ?? 0;
        //    throw new NotImplementedException(nameof(cardColor));
        //}
    }
}
