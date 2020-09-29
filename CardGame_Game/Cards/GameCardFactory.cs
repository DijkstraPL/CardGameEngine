using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGame_Game.Cards
{
    public class GameCardFactory
    {
        public GameCard CreateGameCard(IPlayer owner, Card card)
        {
            //var targettingStrategy = GetTargetStrategy(card);
            switch (card.Kind)
            {
                case Kind.Land:
                    return CreateLandCard(owner, card);
                case Kind.Creature:
                    return CreateCreatureCard(owner, card);
                case Kind.Structure:
                    throw new NotImplementedException();
                case Kind.Spell:
                    return CreateSpellCard(owner , card);
                case Kind.Equipment:
                    throw new NotImplementedException();
                case Kind.Experience:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private GameCard CreateSpellCard(IPlayer owner, Card card)
        {
            var spellCard = new GameSpellCard(owner, card, card.Id, card.Name, card.Description, card.CostBlue, card.InvocationTarget);
            return spellCard;
        }

        private GameCard CreateCreatureCard(IPlayer owner, Card card)
        {
            var creatureCard = new GameCreatureCard(owner, card, card.Id, card.Name, card.Description, card.CostBlue, card.InvocationTarget, card.Attack, card.Cooldown, card.Health);
            return creatureCard;
        }

        private GameCard CreateLandCard(IPlayer owner, Card card)
        {
            var landCard = new GameLandCard(owner, card, card.Id, card.Name, card.Description, card.CostBlue, card.InvocationTarget, card.Cooldown);
            return landCard;
        }

        //private object GetTargetStrategy(Card card)
        //{
        //    switch (card.InvocationTarget)
        //    {
        //        case InvocationTarget.None:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnLands:
        //            break;
        //        case InvocationTarget.OwnLand:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyLands:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyLand:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnEmptyField:
        //            break;
        //        case InvocationTarget.OwnUnit:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnStructure:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnCreature:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnHero:
        //            throw new NotImplementedException();
        //        case InvocationTarget.OwnTakenField:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyEmptyField:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyUnit:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyStructure:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyCreature:
        //            break;
        //        case InvocationTarget.EnemyHero:
        //            throw new NotImplementedException();
        //        case InvocationTarget.EnemyTakenField:
        //            throw new NotImplementedException();
        //        default:
        //            throw new NotImplementedException();
        //    }
        //    throw new NotImplementedException();
        //}
    }

    //public class OwnEmptyFieldTarget
    //{
    //    public Field Field { get; }

    //    public OwnEmptyFieldTarget(Field field)
    //    {
    //        Field = field ?? throw new ArgumentNullException(nameof(field));
    //    }

    //    public bool IsValid()
    //        => Field.Card == null;
    //}

    //public class EnemyCreatureTarget
    //{
    //    //public Field Field { get; }

    //    //public EnemyCreatureTarget(Field field)
    //    //{
    //    //    Field = field ?? throw new ArgumentNullException(nameof(field));
    //    //}

    //    //public bool IsValid()
    //    //    => Field.Card != null;
    //}

    //public class OwnLandsTarget
    //{
    //    IEnumerable<GameLandCard> Lands { get; }

    //    public OwnLandsTarget(IEnumerable<GameLandCard> lands)
    //    {
    //        Lands = lands ?? throw new ArgumentNullException(nameof(lands));
    //    }

    //    public bool IsValid()
    //        => true;
    //}
}
