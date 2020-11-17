using CardGame_Game.GameEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class GameEventsContainer : IGameEventsContainer
    {
        public GameStartingEvent GameStartingEvent { get; }
        public PlayerInitializedEvent PlayerInitializedEvent { get; }
        public GameStartedEvent GameStartedEvent { get; }
        public GameFinishedEvent GameFinishedEvent { get; }

        public TurnStartingEvent TurnStartingEvent { get; }
        public TurnStartedEvent TurnStartedEvent { get; }
        public CardPlayedEvent CardPlayedEvent { get; }
        public TurnFinishedEvent TurnFinishedEvent { get; }

        public UnitBeingAttackingEvent UnitBeingAttackingEvent { get; }
        public UnitAttackedEvent UnitAttackedEvent { get; }
        public UnitKilledEvent UnitKilledEvent { get; }
        public SpellCastingEvent SpellCastingEvent { get; }

        public List<(string name, GameEvent gameEvent)> GameEvents { get; } = new List<(string name, GameEvent gameEvent)>();

        public GameEventsContainer()
        {
            GameStartingEvent = new GameStartingEvent();
            PlayerInitializedEvent = new PlayerInitializedEvent();
            GameStartedEvent = new GameStartedEvent();
            GameFinishedEvent = new GameFinishedEvent();

            TurnStartingEvent = new TurnStartingEvent();
            TurnStartedEvent = new TurnStartedEvent();
            CardPlayedEvent = new CardPlayedEvent();
            TurnFinishedEvent = new TurnFinishedEvent();

            UnitBeingAttackingEvent = new UnitBeingAttackingEvent();
            UnitAttackedEvent = new UnitAttackedEvent();
            UnitKilledEvent = new UnitKilledEvent();
            SpellCastingEvent = new SpellCastingEvent();


            GameEvents.Add((GameStartingEvent.Name, GameStartingEvent));
            GameEvents.Add((PlayerInitializedEvent.Name, PlayerInitializedEvent));
            GameEvents.Add((GameStartedEvent.Name, GameStartedEvent));

            GameEvents.Add((TurnStartingEvent.Name, TurnStartingEvent));
            GameEvents.Add((TurnStartedEvent.Name, TurnStartedEvent));
            GameEvents.Add((CardPlayedEvent.Name, CardPlayedEvent));
            GameEvents.Add((TurnFinishedEvent.Name, TurnFinishedEvent));

            GameEvents.Add((UnitBeingAttackingEvent.Name, UnitBeingAttackingEvent));
            GameEvents.Add((UnitAttackedEvent.Name, UnitAttackedEvent));
            GameEvents.Add((UnitKilledEvent.Name, UnitKilledEvent));
            GameEvents.Add((SpellCastingEvent.Name, SpellCastingEvent));

            GameEvents.ForEach(ge =>
            {
                ge.gameEvent.Add(null, gea =>
                {
                    if (gea.Game.IsGameFinished())
                        GameFinishedEvent.Raise(this, gea);
                });
            });
        }
    }
}
