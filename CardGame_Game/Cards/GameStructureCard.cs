using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards
{
    public class GameStructureCard : GameUnitCard
    {
        public bool Moved { get; set; }

        public GameStructureCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner, card, name, description, cost, invocationTarget, attack, cooldown, health)
        {
        }

        public override bool CanBePlayed(IGame game, IPlayer player, InvocationData invocationData)
            => invocationData.Field != null &&
               invocationData.Field.Card == null &&
               base.CanBePlayed(game, player, invocationData);

        public override void Play(IGame game, IPlayer player, InvocationData invocationData)
        {
            base.Play(game, player, invocationData);
            if (Trait.HasFlag(Trait.Legendary))
            {
                foreach (var field in player.BoardSide.Fields)
                {
                    if (field.Card?.Name == this.Name && field.Card is GameUnitCard gameUnitCard)
                    {
                        gameUnitCard.CardState = Enums.CardState.OnGraveyard;
                        Owner.BoardSide.Kill(gameUnitCard);
                        Owner.AddToGraveyard(gameUnitCard);
                    }
                }
            }
            invocationData.Field.Card = this;
            this.CardState = Enums.CardState.OnField;
        }
    }
}
