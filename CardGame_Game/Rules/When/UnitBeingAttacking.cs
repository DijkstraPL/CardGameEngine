using CardGame_Game.Cards.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.When
{
    [Export(Name, typeof(IEventSource))]
    public class UnitBeingAttacking : IEventSource
    {
        public const string Name = "UnitBeingAttacking";
        string IEventSource.Name => Name;
    }
}
