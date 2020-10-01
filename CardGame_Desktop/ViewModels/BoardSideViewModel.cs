using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CardGame_Desktop.ViewModels
{
    public class BoardSideViewModel : Notifier
    {

        public IBoardSide BoardSide { get;  }
        public ObservableCollection<GameLandCard> LandCards { get; } = new ObservableCollection<GameLandCard>();
        public IPlayer Owner { get; }
        public  ObservableCollection<FieldViewModel> Fields { get; }

        public BoardSideViewModel(IBoardSide boardSide, IPlayer owner)
        {
            BoardSide = boardSide;
            Owner = owner;

            Fields = new ObservableCollection<FieldViewModel>(BoardSide.Fields.Select(f => new FieldViewModel(Owner, f)));
        }

        public void RefreshLandCards()
        {
            LandCards.Clear();

            foreach (var landCard in BoardSide.LandCards)
                LandCards.Add(landCard);
        }
    }
}
