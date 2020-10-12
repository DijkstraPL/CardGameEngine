using CardGame_Client.Services.Interfaces;
using CardGame_Client.ViewModels.Interfaces;
using CardGame_Data.GameData;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardGame_Client.ViewModels.Enemy
{
    public class EnemyBoardViewModel : BindableBase, IPosition
    {
        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        private IList<LandCardViewModel> _landCards = new ObservableCollection<LandCardViewModel>();
        public IEnumerable<LandCardViewModel> LandCards => _landCards;
        private IList<BoardFieldViewModel> _fields = new ObservableCollection<BoardFieldViewModel>();
        public IEnumerable<BoardFieldViewModel> Fields => _fields;
        private IList<BoardFieldViewModel> _orderedFields = new ObservableCollection<BoardFieldViewModel>();
        public IEnumerable<BoardFieldViewModel> OrderedFields => _orderedFields;

        private double _xCoord;
        public double XCoord
        {
            get => _xCoord;
            set => SetProperty(ref _xCoord, value);
        }

        private double _yCoord;
        public double YCoord
        {
            get => _yCoord;
            set => SetProperty(ref _yCoord, value);
        }

        public ICommand SelectToAttackPlayerCommand { get; }

        private readonly IClientGameManager _clientGameManager;
        private readonly ICardGameManagement _cardGameManagement;
        private readonly ITargetSelectionManagement _targetSelectionManagement;
        private GameData _gameData;
        private PlayerData _player;

        public EnemyBoardViewModel(IClientGameManager clientGameManager, ICardGameManagement cardGameManagement, ITargetSelectionManagement targetSelectionManagement)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _cardGameManagement = cardGameManagement ?? throw new ArgumentNullException(nameof(cardGameManagement));
            _targetSelectionManagement = targetSelectionManagement ?? throw new ArgumentNullException(nameof(targetSelectionManagement));
            _clientGameManager.CardTaken += OnCardTaken;
            _clientGameManager.TurnStarted += OnTurnStarted;
            _clientGameManager.CardPlayed += OnCardPlayed;

            _gameData = _clientGameManager.GameData ?? throw new ArgumentNullException(nameof(_clientGameManager.GameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;
            PlayerName = _player.Name;

            SetLandCards(_clientGameManager.GameData);
            SetFields(_clientGameManager.GameData);

            SelectToAttackPlayerCommand = new DelegateCommand(() =>
            {
                _cardGameManagement.OnPlayerSelected(_player);
            });

            _targetSelectionManagement.SetEnemyBoardViewModel(this);
        }

        private void OnCardPlayed(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void OnCardTaken(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void SetLandCards(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            _landCards.Clear();
            foreach (var landCard in _player.BoardSide.LandCards ?? new List<CardData>())
                _landCards.Add(new LandCardViewModel(landCard));
        }

        private void SetFields(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            _fields.Clear();
            foreach (var field in _player.BoardSide.Fields)
                _fields.Add(new BoardFieldViewModel(field, isEnemyField: true, _cardGameManagement));

            _orderedFields.Clear();
            foreach (var field in Fields.Skip(10))
                _orderedFields.Add(field);
            foreach (var field in Fields.Skip(5).Take(5))
                _orderedFields.Add(field);
            foreach (var field in Fields.Take(5))
                _orderedFields.Add(field);
        }
    }
}
