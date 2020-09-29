using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Interfaces
{
    public interface ICooldown
    {
        public int? BaseCooldown { get; }
        public int? Cooldown { get; set; }
    }
}
