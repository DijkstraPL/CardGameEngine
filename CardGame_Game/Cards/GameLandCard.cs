using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Cards
{
    public class GameLandCard : GameCard, ICooldown
    {
        public int? BaseCooldown { get; }
        public int? Cooldown { get; set; }

        public GameLandCard(IPlayer owner, Card card, int id, string name, string description, int? cost, InvocationTarget invocationTarget, int? cooldown)
            : base(owner, card, id, name, description, cost, invocationTarget)
        {
            BaseCooldown = cooldown;
            Cooldown = BaseCooldown;
        }

        public override bool CanBePlayed(IGame game, IPlayer player, InvocationData invocationData)
            => !player.IsLandCardPlayed && base.CanBePlayed(game, player, invocationData);

        public override void Play(IGame game, IPlayer player, InvocationData invocationData)
        {
            base.Play(game, player, invocationData);
            player.BoardSide.AddLandCard(this);
            this.CardState = Enums.CardState.OnField;
            player.IsLandCardPlayed = true;
        }
    }

}
