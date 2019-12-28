using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;

namespace CardGame_Game.Game.Interfaces
{
    public interface IGame
    {
        IPlayer FirstPlayer { get; }
        IPlayer SecondPlayer { get; }
        IPlayer CurrentPlayer { get; }
        IBoard Board { get; }
        event EventHandler<GameEventArgs> TurnFinished;

        void StartGame();
    }
}
