﻿using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;

namespace CardGame_Game.Cards
{
    public class GameSpellCard : GameCard
    {
        protected GameSpellCard _spellInitState => _initState as GameSpellCard;

        public GameSpellCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget)
            : base(owner, card, name, description, cost, invocationTarget)
        {
        }
        protected GameSpellCard(GameSpellCard gameSpellCard) : base(gameSpellCard)
        {
        }

        public override bool CanBePlayed(IGame game, IPlayer player, InvocationData invocationData)
            => base.CanBePlayed(game, player, invocationData);
        public override void Play(IGame game, IPlayer player, InvocationData invocationData)
        {
            base.Play(game, player, invocationData);
            var gameEventArgs = new GameEventArgs { Game = game, Player = player, SourceCard = this, Targets = new List<GameCard> { invocationData.Field?.Card } };
            game.GameEventsContainer.SpellCastingEvent.Raise(this, gameEventArgs);
            this.CardState = Enums.CardState.OnGraveyard;
        }

        public override void Reset()
        {
            base.Reset();
        }

        protected override GameCard GetCardCopy()
            => new GameSpellCard(this);
    }

}
