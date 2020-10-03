using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.GameEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Players
{
    public class BluePlayer : Player
    {
        private int _morale;
        public int Morale 
        { 
            get => _morale; 
            set => _morale = value >= 0 ? value : 0;
        }

        public BluePlayer(string name, Stack<Card> deck, Stack<Card> landDeck, GameCardFactory gameCardFactory, IGameEventsContainer gameEventsContainer) : base(name, deck, landDeck, gameCardFactory, gameEventsContainer)
        {
            PlayerColor = CardColor.Blue;
            GameEventsContainer.UnitKilledEvent.Add(null, gea =>
            {
                if (gea.SourceCard.Owner != this)
                    Morale++;
                else
                    Morale--;
            });
        }


    }
}
