using CardGame_Game.Cards.CardTypes;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Cards.Duty
{
    public class ThroneHall : ILandCard
    {
        public int Id => 10069;
        public string Name => "Throne hall";
        public int Cost => 2;
        public int BaseCountdown => 2;
        public int Countdown { get; set; }
        public ICardType Type => new LandCardType();
        public ISubType SubType => null;
        public CardColor Color => CardColor.White;
        public Rarity Rarity => Rarity.Gold;
        public string Description => "Gives 3 temporary energy.";
        public string Quotation => null;

        private IList<ITrigger> _triggers = new List<ITrigger>();
        public IEnumerable<ITrigger> Triggers { get; }

        private readonly CountdownSetup _countdownCard = new CountdownSetup();

        public ThroneHall()
        {
            Countdown = BaseCountdown;
        }

        public void Play(IGame game, IPlayer player)
        {
            _triggers.ToList().AddRange(_countdownCard.Setup(game, player, this));
            SetUpCountdown(player);
            SetUpMainEffect(player);
            SetUpCountdownReset(game);
        }

        private void SetUpCountdown(IPlayer player)
        {
            var countDownTrigger = new Trigger();
            var countdownEffect = new Effect(g => Countdown--);
            countDownTrigger.AddEvent(new Condition(g => true), countdownEffect);
            _triggers.Add(countDownTrigger);
            player.BoardSide.TurnStarting += countDownTrigger.TriggerIt;
        }

        private void SetUpMainEffect(IPlayer player)
        {
            var mainTrigger = new Trigger();
            var energyIncreaseEffect = new Effect(g => player.IncreaseEnergy(3));
            mainTrigger.AddEvent(new Condition(g => Countdown <= 0), energyIncreaseEffect);
            player.BoardSide.TurnStarted += mainTrigger.TriggerIt;
            _triggers.Add(mainTrigger);
        }

        private void SetUpCountdownReset(IGame game)
        {
            var countDownTrigger = new Trigger();
            var countdownEffect = new Effect(g => Countdown = BaseCountdown);
            countDownTrigger.AddEvent(new Condition(g => Countdown <= 0), countdownEffect);
            _triggers.Add(countDownTrigger);
            game.TurnFinished += countDownTrigger.TriggerIt;
        }
    }
}
