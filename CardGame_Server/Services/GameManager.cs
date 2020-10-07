using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players.Interfaces;
using System;

namespace CardGame_Server.Services
{
    public class GameManager
    {
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        private IGameEventsContainer _gameEventsContainer;
        private IBoard _board;
        public Game Game { get; private set; }

        public GameManager(IGameEventsContainer gameEventsContainer)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
        }

        public void GameInit(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            _firstPlayer = firstPlayer ?? throw new ArgumentNullException(nameof(firstPlayer));
            _secondPlayer = secondPlayer ?? throw new ArgumentNullException(nameof(secondPlayer));

            _board = new Board(_gameEventsContainer);

            Game = new Game(_firstPlayer, _secondPlayer, _board, new RandomHelper(), _gameEventsContainer);
        }

        public void StartGame()
        {
            Game.StartGame();
        }
    }
}
