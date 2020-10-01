using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents.Interfaces
{
    public interface IGameEventsContainer
    {
        GameStartingEvent GameStartingEvent { get; }
        GameStartedEvent GameStartedEvent { get; }
        TurnStartingEvent TurnStartingEvent { get; }
        TurnStartedEvent TurnStartedEvent { get; }
        TurnFinishedEvent TurnFinishedEvent { get; }
        UnitKilledEvent UnitKilledEvent { get; }
        SpellCastingEvent SpellCastingEvent { get; }
        PlayerInitializedEvent PlayerInitializedEvent { get; }
        List<(string name, GameEvent gameEvent)> GameEvents { get; }
    }
}
