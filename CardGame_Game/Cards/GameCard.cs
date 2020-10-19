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
        public Guid Identifier { get; }

        public string Name { get; }
        public int Number { get; }
        public string Description { get; }
        public int? Cost { get; }
        public InvocationTarget InvocationTarget { get; }

        public CardState CardState { get; set; } = CardState.InDeck;
        public IPlayer Owner { get; }
        public Kind Kind { get; }
        public Trait Trait { get; }

        protected readonly Card _card;
        private readonly IList<Trigger> _triggers = new List<Trigger>();

        protected GameCard _initState;

        public GameCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget)
        {
            Identifier = Guid.NewGuid();

            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _card = card ?? throw new System.ArgumentNullException(nameof(card));
            Name = name;
            Number = card.Number;
            Description = description;
            Cost = cost;
            InvocationTarget = invocationTarget;
            Kind = card.Kind;
            Trait = card.Trait;
        }

        public GameCard(GameCard gameCard)
        {
            Identifier = gameCard.Identifier;

            Owner = gameCard.Owner;
            _card = gameCard._card;
            Name = gameCard.Name;
            Number = gameCard.Number;
            Description = gameCard.Description;
            Cost = gameCard.Cost;
            InvocationTarget = gameCard.InvocationTarget;
            Kind = gameCard.Kind;
            Trait = gameCard.Trait;

            foreach (var trigger in gameCard._triggers)
                _triggers.Add(trigger);
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
                _triggers.Add(new Trigger(game.GameEventsContainer, this, rule.Effect));
        }

        //internal abstract void SaveCard();
    }

}
