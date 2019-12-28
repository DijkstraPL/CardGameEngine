using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Cards.Order.Lands
{
    public class Cathedral : LandCard
    {
        public Cathedral() : base("Cathedral", cost: 1, type: Types.Order)
        {
        }

        public override int GetMana()
        {
            return 1;
        }
    }
}
