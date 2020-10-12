using CardGame_Client.Events;
using CardGame_Client.ViewModels;
using CardGame_Data.GameData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Client.Services.Interfaces
{
    public interface ICardGameManagement
    {
        SelectionTargetData SelectionTargetData { get; }

        bool HasTarget(CardData cardData);
        void SetTarget(CardData cardData);
        void OnFieldSelected(object sender, FieldSelectorEventArgs e);
        void OnPlayerSelected(PlayerData playerData);
        void ClearTargets();
    }
}
