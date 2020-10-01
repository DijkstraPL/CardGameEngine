using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CardGame_Game.Cards.Triggers
{
    public class Trigger
    {
        private IList<IEventSource> _eventSources = new List<IEventSource>();
        public IEnumerable<IEventSource> EventSources => _eventSources;

        private IList<(ICondition condition, string[] args)> _conditions = new List<(ICondition condition, string[] args)>();
        public IEnumerable<(ICondition condition, string[] args)> Conditions => _conditions;

        public (IEffect effect, IEnumerable<(ICondition, string[])> conditions, string[] args) Effect { get; private set; }

        private readonly IGameEventsContainer _gameEventsContainer;
        private readonly GameCard _gameCard;


        public Trigger(IGameEventsContainer gameEventsContainer, GameCard gameCard, string whenData, string conditionData, string effectData)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
            _gameCard = gameCard ?? throw new ArgumentNullException(nameof(gameCard));

            SetWhens(whenData);
            SetConditions(conditionData);
            SetEffects(effectData);

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            foreach (var eventSource in _eventSources)
            {
                var gameEvent = _gameEventsContainer.GameEvents.FirstOrDefault(ge => ge.name == eventSource.Name);
                if (gameEvent.gameEvent != null)
                    gameEvent.gameEvent.Add(a =>
                    {
                        a.SourceCard = _gameCard;
                        try
                        {
                            if (_conditions.All(c => c.condition.Validate(a, c.args)))
                                try
                                {
                                    Effect.effect.Invoke(a, Effect.conditions, Effect.args);
                                }
                                catch
                                {
                                }
                        }
                        catch
                        {
                        }
                    });
            }
        }

        private void SetWhens(string whenData)
        {
            var splittedWhenData = whenData.Split(';');
            foreach (var whenString in splittedWhenData)
            {
                var whenName = whenString;
                var when = GetWhen(whenName);
                _eventSources.Add(when);
            }
        }

        private void SetConditions(string conditionData)
        {
            var splittedConditionData = conditionData?.Split(';');
            foreach (var conditionString in splittedConditionData)
            {
                var conditionName = conditionString.Substring(0, conditionString.IndexOf('('));
                var condition = GetCondition(conditionName);
                var conditionArgs = conditionString.EverythingBetween("(", ")").First().Split(',');
                var finalArgs = conditionArgs.ToList().Select(c => c.Replace("\'", string.Empty)).ToArray();
                _conditions.Add((condition, finalArgs));
            }
        }

        private void SetEffects(string effectData)
        {
            var splittedEffectData = effectData?.Split("->");
            string effectString;

            List<(ICondition condition, string[] args)> conditions = new List<(ICondition condition, string[] args)>();
            if (splittedEffectData.Length == 2)
            {
                var splittedConditionData = splittedEffectData[0]?.Split(';');
                effectString = splittedEffectData[1];
                foreach (var conditionString in splittedConditionData)
                {
                    var conditionName = conditionString.Substring(0, conditionString.IndexOf('('));
                    var condition = GetCondition(conditionName);
                    var conditionArgs = conditionString.EverythingBetween("(", ")").First().Split(',');
                    var finalConditionArgs = conditionArgs.ToList().Select(c => c.Replace("\'", string.Empty)).ToArray();
                    conditions.Add((condition, finalConditionArgs));
                }
            }
            else
                effectString = splittedEffectData[0];

            var effectName = effectString.Substring(0, effectString.IndexOf('('));
            var effect = GetEffect(effectName);
            var effectArgs = effectString.EverythingBetween("(", ")").First().Split(',');
            var finalArgs = effectArgs.ToList().Select(c => c.Replace("\'", string.Empty)).ToArray();
            Effect =(effect, conditions, finalArgs);
        }

        private IEventSource GetWhen(string whenName)
        {
            IEventSource eventSource;
            var configuration = new ContainerConfiguration()
               .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

            using (var container = configuration.CreateContainer())
            {
                if (!container.TryGetExport(whenName, out eventSource))
                    throw new InvalidOperationException(nameof(whenName));
            }

            return eventSource;
        }

        private ICondition GetCondition(string conditionName)
        {
            ICondition condition;
            var configuration = new ContainerConfiguration()
               .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

            using (var container = configuration.CreateContainer())
            {
                if (!container.TryGetExport(conditionName, out condition))
                    throw new InvalidOperationException(nameof(conditionName));
            }

            return condition;
        }

        private IEffect GetEffect(string effectName)
        {
            IEffect effect;
            var configuration = new ContainerConfiguration()
               .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

            using (var container = configuration.CreateContainer())
            {
                if (!container.TryGetExport(effectName, out effect))
                    throw new InvalidOperationException(nameof(effectName));
            }

            return effect;
        }
    }
}