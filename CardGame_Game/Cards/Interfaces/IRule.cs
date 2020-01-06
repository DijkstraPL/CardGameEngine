using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Interfaces
{
    public interface IRule
    {
       int Id { get; }
       string Condition { get; }
       string Effect { get; }
       string Description { get; }
    }
}
