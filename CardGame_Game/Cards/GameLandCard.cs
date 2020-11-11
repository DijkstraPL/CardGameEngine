using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Cards
{
    public class GameLandCard : GameCard, ICooldown
    {
        public int? BaseCooldown { get; set; }
        public int? Cooldown { get; set; }

        protected GameLandCard _landCardInitState => _initState as GameLandCard;

        public GameLandCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget, int? cooldown)
            : base(owner, card,  name, description, cost, invocationTarget)
        {
            BaseCooldown = cooldown;
            Cooldown = BaseCooldown;
        }

        protected GameLandCard(GameLandCard gameLandCard) : base(gameLandCard)
        {
            BaseCooldown = gameLandCard.BaseCooldown;
            Cooldown = gameLandCard.Cooldown;
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

        public override void Reset()
        {
            base.Reset();
            BaseCooldown = _landCardInitState.BaseCooldown;
            Cooldown = _landCardInitState.Cooldown;
        }

        protected override GameCard GetCardCopy()
            => new GameLandCard(this);
    }
}
