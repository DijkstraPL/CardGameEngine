using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Players
{
    public class BluePlayer : Player
    {
        public int Morale { get; set; }

        public BluePlayer(string name, Stack<Card> deck, Stack<Card> landDeck, GameCardFactory gameCardFactory) : base(name, deck, landDeck, gameCardFactory)
        {
            PlayerColor = CardColor.Blue;
        }


    }
}
