using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Cards.Duty
{
    public class WatchTower
    {
        //public int Id => 10068;
        //public string Name => "Watch tower";
        //public int Cost => 1;
        //public int BaseCountdown => 1;
        //public int Countdown { get; set; }
        //public ICardType Type => new LandCardType();
        //public ISubType SubType => null;
        //public CardColor Color => CardColor.White;
        //public Rarity Rarity => Rarity.Brown;
        //public string Description => "Gives 1 basic energy. In turn when you play it you can use another land card.";
        //public string Quotation => null;

        //private IList<ITrigger> _triggers = new List<ITrigger>();
        //public IEnumerable<ITrigger> Triggers { get; }

        //private readonly CountdownSetup _countdownCard = new CountdownSetup();

        //public WatchTower()
        //{
        //    Countdown = BaseCountdown;
        //}

        //public void Play(IGame game, IPlayer player)
        //{
        //    _triggers.ToList().AddRange(_countdownCard.Setup(game, player, this));
        //    SetUpMainEffect(player);
        //    RunSecondaryEffect(player);
        //}
        //private void SetUpMainEffect(IPlayer player)
        //{
        //    var mainTrigger = new Trigger();
        //    var energyIncreaseEffect = new Effect(g => player.IncreaseEnergy(1));
        //    mainTrigger.AddEvent(new Condition(g => Countdown <= 0), energyIncreaseEffect);
        //    player.BoardSide.TurnStarted += mainTrigger.TriggerIt;
        //    _triggers.Add(mainTrigger);
        //}

        //private void RunSecondaryEffect(IPlayer player)
        //{
        //    player.IsLandCardPlayed = false;
        //}
    }
}
