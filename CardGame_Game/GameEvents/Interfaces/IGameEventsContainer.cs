using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents.Interfaces
{
    public interface IGameEventsContainer
    {
        GameStartingEvent GameStartingEvent { get; }
        PlayerInitializedEvent PlayerInitializedEvent { get; }
        GameStartedEvent GameStartedEvent { get; }

        TurnStartingEvent TurnStartingEvent { get; }
        TurnStartedEvent TurnStartedEvent { get; }
        CardPlayedEvent CardPlayedEvent { get; }
        TurnFinishedEvent TurnFinishedEvent { get; }

        UnitBeingAttackingEvent UnitBeingAttackingEvent { get; }
        UnitAttackedEvent UnitAttackedEvent { get; }
        UnitKilledEvent UnitKilledEvent { get; }
        SpellCastingEvent SpellCastingEvent { get; }
        List<(string name, GameEvent gameEvent)> GameEvents { get; }
    }
}
