namespace CardGame_Game.Cards.Duty
{
    public class BlacksmithGuild //: ILandCard
    {
        //public int Id => 10067;
        //public string Name => "Blacksmith guild";
        //public int Cost => 1;
        //public int BaseCountdown => 1;
        //public int Countdown { get; set; }
        //public ICardType Type => new LandCardType();
        //public ISubType SubType => null;
        //public CardColor Color => CardColor.White;
        //public Rarity Rarity => Rarity.Silver;
        //public string Description => "Gives 1 basic energy. When you control more than 1 blacksmith guild they give 1 additional basic energy.";
        //public string Quotation => null;

        //private IList<ITrigger> _triggers = new List<ITrigger>();
        //public IEnumerable<ITrigger> Triggers { get; }

        //public static bool AdditionalEnergyGranted { get; private set; }

        //private readonly CountdownSetup _countdownCard = new CountdownSetup();

        //public BlacksmithGuild()
        //{
        //    Countdown = BaseCountdown;
        //}

        //public void Play(IGame game, IPlayer player)
        //{
        //    _triggers.ToList().AddRange(_countdownCard.Setup(game, player, this));
        //    SetUpCleanUp(player);
        //    SetUpMainEffect(player);
        //    SetUpSecondaryEffect(player);
        //}


        //private void SetUpCleanUp(IPlayer player)
        //{
        //    var mainTrigger = new Trigger();
        //    var cleanUpEffect = new Effect(g => AdditionalEnergyGranted = false);
        //    mainTrigger.AddEvent(new Condition(g => true), cleanUpEffect);
        //    _triggers.Add(mainTrigger);
        //    player.BoardSide.TurnStarting += mainTrigger.TriggerIt;
        //}

        //private void SetUpMainEffect(IPlayer player)
        //{
        //    var mainTrigger = new Trigger();
        //    var energyIncreaseEffect = new Effect(g => player.IncreaseEnergy(1));
        //    mainTrigger.AddEvent(new Condition(g => Countdown <= 0), energyIncreaseEffect);
        //    player.BoardSide.TurnStarted += mainTrigger.TriggerIt;
        //    _triggers.Add(mainTrigger);
        //}

        //private void SetUpSecondaryEffect(IPlayer player)
        //{
        //    var secondaryTrigger = new Trigger();
        //    var energyIncreaseEffectSpecial = new Effect(g =>
        //    {
        //        AdditionalEnergyGranted = true;
        //        player.IncreaseEnergy(1);
        //    });
        //    secondaryTrigger.AddEvent(new Condition(g =>
        //    {
        //        return !AdditionalEnergyGranted &&
        //            Countdown <= 0 &&
        //            player.BoardSide.LandCards.Count(c => c.Id == this.Id) > 1;
        //    }),
        //        energyIncreaseEffectSpecial);
        //    player.BoardSide.TurnStarted += secondaryTrigger.TriggerIt;
        //    _triggers.Add(secondaryTrigger);
        //}
    }
}
