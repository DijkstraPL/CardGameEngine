using CardGame_Game.Cards.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.When
{
    [Export(Name, typeof(IEventSource))]
    public class PlayerInitialized : IEventSource
    {
        public const string Name = "PlayerInitialized";
        string IEventSource.Name => Name;
    }
}
