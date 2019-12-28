using System;
using System.Collections.Generic;
using System.Text;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Cards
{
    public class CountdownSetup
    {
        private ILandCard _card;

        public  IEnumerable<ITrigger> Setup(IGame game, IPlayer player, ILandCard card)
        {
            _card = card;

            yield return SetUpCountdown(player);
            yield return SetUpCountdownReset(game);
        }

        private ITrigger SetUpCountdown(IPlayer player)
        {
            var countDownTrigger = new Trigger();
            var countdownEffect = new Effect(g => _card.Countdown--);
            countDownTrigger.AddEvent(new Condition(g => true), countdownEffect);
            player.BoardSide.TurnStarting += countDownTrigger.TriggerIt;

            return countDownTrigger;
        }
        private ITrigger SetUpCountdownReset(IGame game)
        {
            var countDownTrigger = new Trigger();
            var countdownEffect = new Effect(g => _card.Countdown = _card.BaseCountdown);
            countDownTrigger.AddEvent(new Condition(g => _card.Countdown <= 0), countdownEffect);
            game.TurnFinished += countDownTrigger.TriggerIt;

            return countDownTrigger;
        }
    }
}
