using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Helpers
{
    public class RandomHelper : IRandomHelper
    {
        public bool FlipCoin()
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }
    }

    public interface IRandomHelper
    {
        bool FlipCoin();
    }
}
