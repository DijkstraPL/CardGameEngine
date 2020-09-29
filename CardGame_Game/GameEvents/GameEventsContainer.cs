using CardGame_Game.GameEvents.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class GameEventsContainer : IGameEventsContainer
    {
        public GameStartingEvent GameStartingEvent { get; }
        public GameStartedEvent GameStartedEvent { get; }
        public TurnStartingEvent TurnStartingEvent { get; }
        public TurnStartedEvent TurnStartedEvent { get; }
        public TurnFinishedEvent TurnFinishedEvent { get; }
        public List<(string name, GameEvent gameEvent)> GameEvents { get; } = new List<(string name, GameEvent gameEvent)>();

        public GameEventsContainer()
        {
            GameStartingEvent = new GameStartingEvent();
            GameStartedEvent = new GameStartedEvent();
            TurnStartingEvent = new TurnStartingEvent();
            TurnStartedEvent = new TurnStartedEvent();
            TurnFinishedEvent = new TurnFinishedEvent();

            GameEvents.Add((GameStartingEvent.Name, GameStartingEvent));
            GameEvents.Add((GameStartedEvent.Name, GameStartedEvent));
            GameEvents.Add((TurnStartingEvent.Name, TurnStartingEvent));
            GameEvents.Add((TurnStartedEvent.Name, TurnStartedEvent));
            GameEvents.Add((TurnFinishedEvent.Name, TurnFinishedEvent));
        }
    }
}
