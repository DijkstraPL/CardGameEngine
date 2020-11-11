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
        protected GameStructureCard _gameStructureInitState => _initState as GameStructureCard;

        public GameStructureCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner, card, name, description, cost, invocationTarget, attack, cooldown, health)
        {
        }
        protected GameStructureCard(GameStructureCard gameStructureCard) : base(gameStructureCard)
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
        public override void Reset()
        {
            base.Reset();
        }
        protected override GameCard GetCardCopy()
            => new GameStructureCard(this);
    }
}
