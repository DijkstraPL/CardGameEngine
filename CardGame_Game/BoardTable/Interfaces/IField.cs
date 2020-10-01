using CardGame_Game.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.BoardTable.Interfaces
{
    public interface IField
    {
        GameUnitCard Card { get; set; }
    }
}
