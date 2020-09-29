using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace CardGame_Game.BoardTable
{
    public class BoardSide : IBoardSide
    {
        public IReadOnlyList<Field> Fields { get; }
        public IList<GameLandCard> LandCards { get; } = new List<GameLandCard>();

        private readonly IGameEventsContainer _gameEventsContainer;

        public BoardSide(IGameEventsContainer gameEventsContainer)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));

            Fields = new List<Field>
            {
                new Field(0,0),
                new Field(0,1),
                new Field(0,2),
                new Field(0,3),
                new Field(0,4),
                new Field(1,0),
                new Field(1,1),
                new Field(1,2),
                new Field(1,3),
                new Field(1,4),
                new Field(2,0),
                new Field(2,1),
                new Field(2,2),
                new Field(2,3),
                new Field(2,4),
            };
        }

        public IEnumerable<Field> GetNeighbourFields(Field field)
        {
            return Fields.Where(f =>
             f.X == field.X - 1 && f.Y == field.Y - 1 ||
             f.X == field.X - 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y - 1 ||
             f.X == field.X + 1 && f.Y == field.Y ||
             f.X == field.X - 1 && f.Y == field.Y ||
             f.X == field.X && f.Y == field.Y + 1 ||
             f.X == field.X && f.Y == field.Y - 1
             );
        }

        public void AddLandCard(GameLandCard card)
        {
            LandCards.Add(card);
        }

        public void StartTurn(IGame game)
        {
            _gameEventsContainer.TurnStartingEvent.Raise(this, new GameEventArgs { Game = game, Player = game.CurrentPlayer });

            foreach (var landCard in LandCards)
            {
                if (landCard.Cooldown == 0)
                    landCard.Cooldown = landCard.BaseCooldown;
                landCard.Cooldown--;
            }

            //foreach (var landCard in LandCards)
            //{
            //    foreach (var rule in landCard.Rules)
            //    {
            //        var conditionName = rule.Condition.Substring(0, rule.Condition.IndexOf('('));
            //        var condition = GetCondition(conditionName);

            //        var effectName = rule.Effect.Substring(0, rule.Effect.IndexOf('('));
            //        var effect = GetEffect(effectName);

            //        var conditionArgs = rule.Condition.EverythingBetween("(", ")").First().Split(',');
            //        var effectArgs = rule.Effect.EverythingBetween("(", ")").First().Split(',');

            //        if (condition.Validate(conditionArgs))
            //            effect.Invoke(effectArgs);
            //    }
            //}

            _gameEventsContainer.TurnStartedEvent.Raise(this, new GameEventArgs { Game = game, Player = game.CurrentPlayer });
        }

        public void Move(Field start, Field target)
        {
            var card = start.Card;
            target.Card = card;
            start.Card = null;
        }

        //public Trigger GetTrigger(string conditionName, string effectName)
        //{
        //    ICondition condition = GetCondition(conditionName);
        //    IEffect effect = GetEffect(effectName);

        //    return new Trigger(condition, effect);
        //}

        //private static ICondition GetCondition(string conditionName)
        //{
        //    ICondition condition;
        //    var configuration = new ContainerConfiguration()
        //       .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

        //    using (var container = configuration.CreateContainer())
        //    {
        //        if (!container.TryGetExport(conditionName, out condition))
        //            throw new InvalidOperationException(nameof(conditionName));
        //    }

        //    return condition;
        //}

        //private static IEffect GetEffect(string effectName)
        //{
        //    IEffect effect;
        //    var configuration = new ContainerConfiguration()
        //       .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

        //    using (var container = configuration.CreateContainer())
        //    {
        //        if (!container.TryGetExport(effectName, out effect))
        //            throw new InvalidOperationException(nameof(effectName));
        //    }

        //    return effect;
        //}
    }
}
