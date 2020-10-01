using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CardGame_Game.Cards
{
    public abstract class GameCard 
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int? Cost { get; }
        public InvocationTarget InvocationTarget { get; }

        public CardState CardState { get; set; } = CardState.InDeck;
        public IPlayer Owner { get; }

        protected readonly Card _card;
        private readonly IList<Trigger> _triggers = new List<Trigger>();

        public GameCard(IPlayer owner, Card card, int id, string name, string description, int? cost, InvocationTarget invocationTarget)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _card = card ?? throw new System.ArgumentNullException(nameof(card));
            Id = id;
            Name = name;
            Description = description;
            Cost = cost;
            InvocationTarget = invocationTarget;
        }

        public virtual bool CanBePlayed(IGame game, IPlayer player, InvocationData invocationData)
            => player.Energy >= Cost;
        public virtual void Play(IGame game, IPlayer player, InvocationData invocationData)
        {
            if (Cost != null)
                player.IncreaseEnergy(_card.Color, -(int)Cost);
            player.Hand.Remove(this);
        }

        internal void RegisterTriggers(IGame game)
        {
            foreach (var rule in _card.Rules)
            {
                var trigger = new Trigger(game.GameEventsContainer, this, rule.When, rule.Condition, rule.Effect);
                _triggers.Add(trigger);
            }
        }
    }

}
