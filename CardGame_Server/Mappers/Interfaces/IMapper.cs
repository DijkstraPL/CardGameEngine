using CardGame_Data.GameData;
using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Server.Mappers.Interfaces
{
    public interface IMapper
    {
        BoardData MapBoard(IBoard board);
        BoardSideData MapBoardSide(IBoardSide boardSite);
        CardData MapCard(GameCard card);
        FieldData MapField(Field field);
        GameData MapGame(IGame game, bool isCurrentPlayer);
        PlayerData MapPlayer(IPlayer player, bool setHand);
        AttackTargetData MapTarget(IHealthy attackTarget);
    }
}