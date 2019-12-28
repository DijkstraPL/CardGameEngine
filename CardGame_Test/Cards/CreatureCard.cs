using CardGame_Test.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Cards
{
    public abstract class CreatureCard : Card
    {
        #region Properties

        public int Attack { get; protected set; }
        public int ReactionTime { get; protected set; }
        public int Health { get; protected set; }
        public int Move { get; protected set; }

        public event EventHandler AfterAttack;

        #endregion // Properties

        #region Public_Methods

        public CreatureCard(string name, int attack, int reactionTime, int health,
            int move, int cost, Types type, CardTypes cardType, IList<CardSubtypes> cardSubtypes)
            : base(name, cost, type, cardType, cardSubtypes)
        {
            Attack = attack;
            ReactionTime = reactionTime;
            Health = health;
            Move = move;
        }

        public void OnAfterAttack()
        {
            AfterAttack(this, EventArgs.Empty);
        }

        #endregion // Public_Methods


    }
}
