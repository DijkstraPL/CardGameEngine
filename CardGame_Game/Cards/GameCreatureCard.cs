﻿using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Cards
{
    public class GameCreatureCard: GameUnitCard, IMovable
    {
        public bool Moved { get; set; }

        public GameCreatureCard(IPlayer owner, Card card,  string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner ,card,  name, description, cost, invocationTarget, attack, cooldown, health)
        {
        }

        public override bool CanBePlayed(IGame game, IPlayer player, InvocationData invocationData)
            => invocationData.Field != null &&
               invocationData.Field.Card == null &&
               base.CanBePlayed(game, player, invocationData);

        public override void Play(IGame game, IPlayer player, InvocationData invocationData)
        {
            base.Play(game, player, invocationData);
            invocationData.Field.Card = this;
            this.CardState = Enums.CardState.OnField;
        }
    }

}
