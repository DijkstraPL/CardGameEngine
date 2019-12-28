using CardGame_Test.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Cards
{
    public abstract class LandCard : Card
    {
        public LandCard(string name ,int cost, Types type) : base(name, cost, type)
        {
        }

        public abstract int GetMana();
    }
}
