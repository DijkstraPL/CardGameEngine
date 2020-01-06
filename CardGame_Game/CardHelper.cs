using CardGame_Data.Entities.Enums;
using CardGame_Game.Cards.Interfaces;
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

        public static int GetCost(this ICard card, CardColor cardColor)
        {
            if (cardColor == CardColor.White)
                return card.CostWhite ?? 0;
            if (cardColor == CardColor.Red)
                return card.CostRed ?? 0;
            if (cardColor == CardColor.Green)
                return card.CostGreen ?? 0;
            throw new NotImplementedException(nameof(cardColor));
        }
    }
}
