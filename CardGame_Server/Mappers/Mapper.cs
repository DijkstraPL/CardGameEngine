﻿using CardGame_Data.GameData;
using CardGame_Data.GameData.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using CardGame_Server.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Mappers
{
    public class Mapper : IMapper
    {
        public GameData MapGame(IGame game, bool isCurrentPlayer)
        {
            if (game == null)
                return null;

            var gameData = new GameData
            {
                IsControllingCurrentPlayer = isCurrentPlayer,
                Board = MapBoard(game.Board),
                CurrentPlayer = MapPlayer(game.CurrentPlayer, setHand: isCurrentPlayer),
                NextPlayer = MapPlayer(game.NextPlayer, setHand: !isCurrentPlayer),
                TurnCounter = game.TurnCounter
            };

            return gameData;
        }

        private PlayerData MapPlayer(IPlayer player, bool setHand)
        {
            if (player == null)
                return null;

            PlayerData playerData = new PlayerData();
            if (player is BluePlayer bluePlayer)
                playerData.Morale = bluePlayer.Morale;

            playerData.BaseHealth = player.BaseHealth;
            playerData.Graveyard = player.Graveyard.Count;
            playerData.BoardSide = MapBoardSide(player.BoardSide);
            playerData.Deck = player.Deck.Count;
            playerData.Energy = player.Energy;
            playerData.FinalHealth = player.FinalHealth;
            playerData.Hand = player.Hand.Count;
            if (setHand)
                playerData.HandCards = player.Hand.Select(c => MapCard(c)).ToList();
            playerData.LandDeck = player.LandDeck.Count;
            playerData.Name = player.Name;
            playerData.PlayerColor = player.PlayerColor;

            return playerData;
        }

        private BoardData MapBoard(IBoard board)
        {
            if (board == null)
                return null;

            var boardData = new BoardData
            {
                LeftBoardSide = MapBoardSide(board.LeftBoardSite),
                RightBoardSide = MapBoardSide(board.RightBoardSite)
            };

            return boardData;
        }

        private BoardSideData MapBoardSide(IBoardSide boardSide)
        {
            if (boardSide == null)
                return null;

            var boardSideData = new BoardSideData
            {
                Fields = boardSide.Fields.Select(f => MapField(f)).ToList(),
                LandCards = boardSide.LandCards.Select(l => MapCard(l)).ToList()
            };

            return boardSideData;
        }

        private FieldData MapField(Field field)
        {
            if (field == null)
                return null;

            var fieldData = new FieldData
            {
                X = field.X,
                Y = field.Y,
            };
            if (field.Card != null)
                fieldData.UnitCard = MapCard(field.Card);

            return fieldData;
        }

        private CardData MapCard(GameCard card)
        {
            if (card == null)
                return null;

            CardData cardData = new CardData();

            if (card is IAttacker attackCard)
            {
                cardData.AttackTarget = MapTarget(attackCard.AttackTarget);
                cardData.BaseAttack = attackCard.BaseAttack;
                cardData.FinalAttack = attackCard.FinalAttack;
            }
            if (card is IHealthy healthyCard)
            {
                cardData.BaseHealth = healthyCard.BaseHealth;
                cardData.FinalHealth = healthyCard.FinalHealth;
            }
            if (card is ICooldown cooldown)
            {
                cardData.BaseCooldown = cooldown.BaseCooldown;
                cardData.Cooldown = cooldown.Cooldown;
            }

            cardData.Identifier = card.Identifier;
            cardData.CardState = (CardState)card.CardState;
            cardData.Cost = card.Cost;
            cardData.Description = card.Description;
            cardData.InvocationTarget = card.InvocationTarget;
            cardData.Name = card.Name;
            cardData.OwnerName = card.Owner.Name;

            return cardData;
        }

        private AttackTargetData MapTarget(IHealthy attackTarget)
        {
            if (attackTarget == null)
                return null;

            var attackTargetData = new AttackTargetData();

            if (attackTargetData is IPlayer player)
                attackTargetData.PlayerTarget = MapPlayer(player, setHand: false);
            else if (attackTarget is GameCard gameCard)
                attackTargetData.CardTarget = MapCard(gameCard);

            return attackTargetData;
        }
    }
}
