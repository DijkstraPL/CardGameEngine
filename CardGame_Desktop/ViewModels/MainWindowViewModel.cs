using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Game.Game;
using CardGame_Game.GameEvents;
using CardGame_Game.Helpers;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CardGame_Desktop.ViewModels
{
    public class MainWindowViewModel : Notifier
    {
        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        private PlayerViewModel _currentPlayer;
        public PlayerViewModel CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                SetProperty(ref _currentPlayer, value);
                OnPropertyChanged(nameof(IsPlayer1));
                OnPropertyChanged(nameof(IsPlayer2));
            }
        }

        public bool IsPlayer1 => CurrentPlayer.Player == Player1.Player;
        public bool IsPlayer2 => CurrentPlayer.Player == Player2.Player;

        public ICommand GetCardFromLandDeckCommand { get; }
        public ICommand GetCardFromDeckCommand { get; }
        public ICommand FinishTurnCommand { get; }
        public ICommand PlayCardCommand { get; }

        public MainWindowViewModel()
        {
            var gameEventsContainer = new GameEventsContainer();

            var player1 = SetUpPlayer1();
            var player2 = SetUpPlayer2();

            var board = new Board(gameEventsContainer);

            var game = new Game(player1, player2, board, new RandomHelper(), gameEventsContainer);
            game.StartGame();

            Player1 = new PlayerViewModel(player1);
            Player2 = new PlayerViewModel(player2);

            CurrentPlayer = game.CurrentPlayer == player1 ? Player1 : Player2;

            GetCardFromLandDeckCommand = new RelayCommand(action =>
            {
                game.GetCardFromLandDeck();
                CurrentPlayer.RefreshHand();
            });
            GetCardFromDeckCommand = new RelayCommand(action => 
            { 
                game.GetCardFromDeck();
                CurrentPlayer.RefreshHand();
            });
            FinishTurnCommand = new RelayCommand(action =>
            {
                game.FinishTurn();
                CurrentPlayer = game.CurrentPlayer == player1 ? Player1 : Player2;
                CurrentPlayer.RefreshEnergy();
            });
            PlayCardCommand = new RelayCommand(card =>
            {
                game.PlayCard((GameCard)card, new InvocationData());
                CurrentPlayer.RefreshHand();
                CurrentPlayer.RefreshEnergy();
            });
        }


        private BluePlayer SetUpPlayer1()
        {
            var landDeck = new Stack<Card>();
            for (int i = 0; i < 12; i++)
                landDeck.Push(CreateKathedralCity());
            landDeck.Push(CreateThroneHall());
            landDeck.Push(CreateThroneHall());
            landDeck.Push(CreateThroneHall());
            var deck = new Stack<Card>();
            for (int i = 0; i < 25; i++)
                deck.Push(CreateVillager());
            for (int i = 0; i < 15; i++)
                deck.Push(CreateHasteBlessing());

            var player1 = new BluePlayer("Johan", deck, landDeck, new GameCardFactory());
            return player1;
        }
        private BluePlayer SetUpPlayer2()
        {
            var landDeck = new Stack<Card>();
            for (int i = 0; i < 12; i++)
                landDeck.Push(CreateThroneHall());
            landDeck.Push(CreateKathedralCity());
            landDeck.Push(CreateKathedralCity());
            landDeck.Push(CreateKathedralCity());
            var deck = new Stack<Card>();
            for (int i = 0; i < 30; i++)
                deck.Push(CreateVillager());
            for (int i = 0; i < 10; i++)
                deck.Push(CreateHasteBlessing());

            var player1 = new BluePlayer("Michael", deck, landDeck, new GameCardFactory());
            return player1;
        }

        private Card CreateKathedralCity()
        {
            var card = new Card
            {
                Id = 1,
                Name = "Kathedral city",
                CostBlue = 0,
                Cooldown = 1,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Brown,
                Description = "Gives 1 basic energy.",
                Color = CardColor.Blue,
                Number = 61,
                Flavour = "Miasto z góry lepiej się prezentuje, ludzie wydają się ładniejsi, a najgorsi wyglądają niemalże na dobrych.",
                CardType = new CardType { Id = 1, Name = "Land" },
                CardTypeId = 1,
                Set = new Set { Id = 1, Name = "The Big Bang" },
                SetId = 1,
            };

            var rule = new Rule
            {
                Id = 1,
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(rule);

            return card;
        }

        private Card CreateThroneHall()
        {
            var card = new Card
            {
                Id = 2,
                Name = "Throne hall",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Brown,
                Description = "Gives 3 temporary energy.",
                Color = CardColor.Blue,
                Number = 69,
                Flavour = "W grze o tron zwycięża się albo umiera. Nie ma ziemi niczyjej.",
                CardType = new CardType { Id = 1, Name = "Land" },
                CardTypeId = 1,
                Set = new Set { Id = 1, Name = "The Big Bang" },
                SetId = 1,
            };

            var rule = new Rule
            {
                Id = 2,
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',3)",
                Description = "Gives 3 temporary energy."
            };
            card.Rules.Add(rule);

            return card;
        }

        private Card CreateVillager()
        {
            var card = new Card
            {
                Id = 3,
                Name = "Villager",
                CostBlue = 1,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "",
                Color = CardColor.Blue,
                Number = 1,
                Flavour = "Na wojnie wszystko jest możliwe. Wojna miesza, zrównuje, chłop czy filozof - wszyscy są do umierania.",
                CardType = new CardType { Id = 2, Name = "Human" },
                CardTypeId = 2,
                Set = new Set { Id = 1, Name = "The Big Bang" },
                SetId = 1,
                Health = 2,
                Attack = 2,
            };

            var rule = new Rule
            {
                Id = 3,
                When = "TurnStarted",
                Condition = "Morale('SELF',2)",
                Effect = "AddAttack('SELF',1)",
                Description = "Gives 3 temporary energy."
            };
            card.Rules.Add(rule);

            return card;
        }

        private Card CreateHasteBlessing()
        {
            var card = new Card
            {
                Id = 4,
                Name = "Haste blessing",
                CostBlue = 1,
                Kind = Kind.Spell,
                InvocationTarget = InvocationTarget.OwnCreature | InvocationTarget.EnemyCreature,
                Rarity = Rarity.Brown,
                Description = "Decrease creature cooldown by 1.",
                Color = CardColor.Blue,
                Number = 1,
                Flavour = "Nic nie jest bardziej niebezpieczne niż wróg, który nie ma nic do stracenia.",
                CardType = new CardType { Id = 3, Name = "Spell" },
                CardTypeId = 3,
                SubType = new Subtype { Id = 1, Name = "Transformation" },
                SubTypeId = 3,
                Set = new Set { Id = 1, Name = "The Big Bang" },
                SetId = 1,
            };

            var rule = new Rule
            {
                Id = 4,
                When = "TargetSelected",
                Condition = "OnField('TARGET')",
                Effect = "AddCooldown('TARGET',-1)",
                Description = "Decrease creature cooldown by 1."
            };
            card.Rules.Add(rule);

            return card;
        }
    }
}
