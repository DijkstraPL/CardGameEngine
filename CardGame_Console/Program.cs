using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Validators;
using CardGame_Game.GameEvents;
using CardGame_Game.Helpers;
using CardGame_Game.Players;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace CardGame_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEventsContainer = new GameEventsContainer();

            BluePlayer player1 = SetUpPlayer1();
            BluePlayer player2 = SetUpPlayer2();

            var board = new Board(gameEventsContainer);

            var game = new Game(player1, player2, board, new RandomHelper(), gameEventsContainer);
            game.StartGame();
            var gameValidator = new CardCountValidator(game);
            if (!gameValidator.IsValid())
            {
                Console.WriteLine("Not valid!");
                //   return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Current player: " + game.CurrentPlayer.Name);
                Console.WriteLine("Current energy: " + game.CurrentPlayer.Energy);

                var handCards = game.CurrentPlayer.Hand;
                for (int i = 1; i <= handCards.Count(); i++)
                {
                    var cardFromHand = handCards.ElementAt(i - 1);
                    Console.WriteLine(i + "-" + cardFromHand.Name + " (" + cardFromHand.Cost + ")");
                }

                Console.WriteLine("You can:");
                if (!game.CurrentPlayer.CardTaken)
                {
                    Console.WriteLine("1. Get card from deck");
                    Console.WriteLine("2. Get card from land deck");
                }
                if (game.CurrentPlayer.Hand.Count > 0)
                    Console.WriteLine("3. Play card");
                Console.WriteLine("4. Look at the table");
                Console.WriteLine("9. Finish turn");


                Console.WriteLine("Battlefield:");

                var sign = Console.ReadLine();

                if (sign == "1" && !game.CurrentPlayer.CardTaken)
                    game.CurrentPlayer.GetCardFromDeck();
                else if (sign == "2" && !game.CurrentPlayer.CardTaken)
                    game.CurrentPlayer.GetCardFromLandDeck();
                else if (sign == "3")
                {
                    Console.WriteLine("Select card");
                    int cardIndex = Convert.ToInt32(Console.ReadLine());
                    var card = handCards.ElementAt(cardIndex - 1);
                    game.PlayCard(card, new InvocationData());
                }
                else if (sign == "4")
                {
                    Console.WriteLine("Land cards:");
                    for (int i = 1; i <= game.CurrentPlayer.BoardSide.LandCards.Count(); i++)
                    {
                        var landCardFromBoard = game.CurrentPlayer.BoardSide.LandCards.ElementAt(i - 1);
                        Console.WriteLine(i + "-" + landCardFromBoard.Name + " (" + landCardFromBoard.BaseCooldown + ")");
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else if (sign == "9")
                    game.FinishTurn();
            }

            //OldGame();

            Console.ReadLine();
        }

        private static BluePlayer SetUpPlayer1()
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
        private static BluePlayer SetUpPlayer2()
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


        private static Card CreateKathedralCity()
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
                Condition = "Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(rule);

            return card;
        }

        private static Card CreateThroneHall()
        {
            var card = new Card
            {
                Id = 2,
                Name = "Throne hall",
                CostBlue = 2,
                Cooldown = 1,
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
                When="TurnStarted",
                Condition = "Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',3)",
                Description = "Gives 3 temporary energy."
            };
            card.Rules.Add(rule);

            return card;
        }

        private static Card CreateVillager()
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
                Trait = "Morale 2+: +2 attack",
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
                Description = "Morale 2+: +2 attack"
            };
            card.Rules.Add(rule);

            return card;
        }

        private static Card CreateHasteBlessing()
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
