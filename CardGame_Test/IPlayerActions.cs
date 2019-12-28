using CardGame_Test.BoardTable;
using CardGame_Test.Cards;
using CardGame_Test.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test
{
    public interface IPlayerActions
    {
        Card TakeCard();
        LandCard TakeLandCard();
        void PlayCard(Card card, Field field);
        void RemoveCard(Card card);
        void GiveTurn();
        void Surrender();

        void AttackWithCreature(Unit unit, Field field);
    }
}
