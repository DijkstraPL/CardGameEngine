using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CardGame_Client.ViewModels.Player
{
    public class PlayerDecksViewModel : BindableBase
    {
        private int _deckCardCount;
        public int DeckCardCount
        {
            get => _deckCardCount;
            set => SetProperty(ref _deckCardCount, value);
        }
        private int _landDeckCardCount;
        public int LandDeckCardCount
        {
            get => _landDeckCardCount;
            set => SetProperty(ref _landDeckCardCount, value);
        }
        private int _graveyardCardCount;
        public int GraveyardCardCount
        {
            get => _graveyardCardCount;
            set => SetProperty(ref _graveyardCardCount, value);
        }

        public ICommand GetCardFromDeckCommand { get; }
        public ICommand GetCardFromLandDeckCommand { get; }

        private readonly IClientGameManager _clientGameManager;
        private GameData _gameData;
        private PlayerData _player;

        public PlayerDecksViewModel(IClientGameManager clientGameManager)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _clientGameManager.CardTaken += OnCardTaken;
            _clientGameManager.TurnStarted += OnTurnStarted;

            SetDecks(_clientGameManager.GameData);

            GetCardFromDeckCommand = new DelegateCommand(() => { _clientGameManager.DrawCard(); });
            GetCardFromLandDeckCommand = new DelegateCommand(() => { _clientGameManager.DrawLandCard(); });
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            SetDecks(gameData);
        }

        private void OnCardTaken(object sender, GameData gameData)
        {
            SetDecks(gameData);
        }

        private void SetDecks(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.CurrentPlayer : _gameData.NextPlayer;

            DeckCardCount = _player.Deck;
            LandDeckCardCount = _player.LandDeck;
            GraveyardCardCount = _player.Graveyard;
        }
    }
}
