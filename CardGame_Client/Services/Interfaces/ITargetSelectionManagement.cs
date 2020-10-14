using CardGame_Client.ViewModels;
using CardGame_Client.ViewModels.Enemy;
using CardGame_Client.ViewModels.Player;
using CardGame_Data.GameData;

namespace CardGame_Client.Services.Interfaces
{
    public interface ITargetSelectionManagement
    {
       void SetPlayerBoardViewModel(PlayerBoardViewModel playerBoardViewModel);
       void SetEnemyBoardViewModel(EnemyBoardViewModel enemyBoardViewModel);
       void SetGameViewModel(GameViewModel mainWindowViewModel);
        bool CanMove(CardData attackSource, FieldData fieldData);
    }
}