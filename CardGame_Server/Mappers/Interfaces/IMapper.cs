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
        GameData MapGame(IGame game, bool isCurrentPlayer);
    }
}