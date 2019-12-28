using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using System.Collections.Generic;

namespace CardGame_Game.Cards.Interfaces
{
    public interface ILandCard : ICard
    {
        IEnumerable<ITrigger> Triggers { get; }
        int Countdown { get; set; }
        int BaseCountdown { get; }
    }
}
