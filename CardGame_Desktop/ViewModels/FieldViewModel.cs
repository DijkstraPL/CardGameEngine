using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Desktop.ViewModels
{
    public class FieldViewModel : Notifier
    {
        public Field Field { get; }
        public BoardSideViewModel BoardSideViewModel { get; }
        public IPlayer Owner { get; }

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

        public GameCard Card => Field.Card;

        public FieldViewModel(IPlayer owner, Field field, BoardSideViewModel boardSideViewModel)
        {
            Owner = owner;
            Field = field;
            BoardSideViewModel = boardSideViewModel;
        }

        internal void Refresh()
        {
            OnPropertyChanged(nameof(Card));
        }
    }
}
