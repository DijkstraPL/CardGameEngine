using CardGame_Game;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Duty;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = SetUpPlayer1();
            Player player2 = SetUpPlayer2();

            var board = new Board();

            var game = new Game(player1, player2, board);

            var gameValidator = new GameValidator(game);
            if (!gameValidator.IsValid())
            {
                Console.WriteLine("Not valid!");
                //   return;
            }

            game.StartGame();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Current player: " + game.CurrentPlayer.Name);
                Console.WriteLine("Current energy: " + game.CurrentPlayer.Energy);

                var handCards = game.CurrentPlayer.GetHand();
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
                if (game.CurrentPlayer.GetHand().Count() > 0)
                    Console.WriteLine("3. Play card");
                Console.WriteLine("4. Look at the table");
                Console.WriteLine("9. Finish turn");

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
                    if (card is ILandCard landCard)
                        game.PlayLandCard(landCard);
                }
                else if (sign == "4")
                {
                    Console.WriteLine("Land cards:");
                    for (int i = 1; i <= game.CurrentPlayer.BoardSide.LandCards.Count(); i++)
                    {
                        var landCardFromBoard = game.CurrentPlayer.BoardSide.LandCards.ElementAt(i - 1);
                        Console.WriteLine(i + "-" + landCardFromBoard.Name + " (" + landCardFromBoard.Countdown + ")");
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else if (sign == "9")
                    game.NextTurn();
            }

            //OldGame();

            Console.ReadLine();
        }

        private static Player SetUpPlayer1()
        {
            var landDeck = new Stack<ILandCard>();
            landDeck.Push(new KathedralCity());
            landDeck.Push(new KathedralCity());
            landDeck.Push(new WatchTower());
            landDeck.Push(new WatchTower());
            landDeck.Push(new ThroneHall());
            landDeck.Push(new ThroneHall());
            landDeck.Push(new KathedralCity());
            var deck = new Stack<ICard>();

            var player1 = new Player("Johan", deck, landDeck);
            return player1;
        }
        private static Player SetUpPlayer2()
        {
            var landDeck = new Stack<ILandCard>();
            landDeck.Push(new KathedralCity());
            landDeck.Push(new KathedralCity());
            landDeck.Push(new WatchTower());
            landDeck.Push(new WatchTower());
            landDeck.Push(new BlacksmithGuild());
            landDeck.Push(new BlacksmithGuild());
            landDeck.Push(new KathedralCity());
            var deck = new Stack<ICard>();

            var player1 = new Player("Michael", deck, landDeck);
            return player1;
        }

        //private static void OldGame()
        //{
        //    var cards1 = new Stack<Card>();
        //    cards1.Push(new Peasant());
        //    cards1.Push(new Peasant());
        //    cards1.Push(new Peasant());
        //    var landCards1 = new Stack<LandCard>();
        //    landCards1.Push(new Cathedral());
        //    landCards1.Push(new Cathedral());
        //    landCards1.Push(new Cathedral());
        //    var playerOne = new Player("KOKAN", cards1, landCards1);
        //    var cards2 = new Stack<Card>();
        //    cards2.Push(new Peasant());
        //    cards2.Push(new Peasant());
        //    cards2.Push(new Peasant());
        //    var landCards2 = new Stack<LandCard>();
        //    landCards2.Push(new Cathedral());
        //    landCards2.Push(new Cathedral());
        //    landCards2.Push(new Cathedral());
        //    var playerTwo = new Player("Dijkstra", cards2, landCards2);

        //    var game = new Game(playerOne, playerTwo);

        //    game.Start();
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Current player: " + game.CurrentPlayer.Name);

        //        var handCards = game.CurrentPlayer.GetHand();
        //        for (int i = 1; i <= handCards.Count(); i++)
        //            Console.WriteLine(i + "-" + handCards.ElementAt(i - 1).Name);

        //        Console.WriteLine("You can:");
        //        if (!game.CurrentPlayer.CardTaken)
        //        {
        //            Console.WriteLine("1. Get card from deck");
        //            Console.WriteLine("2. Get card from land deck");
        //        }
        //        Console.WriteLine("9. Finish turn");

        //        var sign = Console.ReadLine();

        //        if (sign == "1" && !game.CurrentPlayer.CardTaken)
        //            game.CurrentPlayer.TakeCardFromDeck();
        //        else if (sign == "2" && !game.CurrentPlayer.CardTaken)
        //            game.CurrentPlayer.TakeCardFromLandDeck();

        //        else if (sign == "9")
        //            game.NextTurn();
        //    }
        //}
    }
}
