using CardGame_Game.Cards;
using CardGame_Game.GameEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Rules.Interfaces
{
    public interface IRule
    {
        void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args);
    }
}
