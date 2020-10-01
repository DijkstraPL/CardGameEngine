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
        public IPlayer Owner { get; }

        public GameCard Card => Field.Card;

        public FieldViewModel(IPlayer owner, Field field)
        {
            Owner = owner;
            Field = field;
        }

        internal void Refresh()
        {
            OnPropertyChanged(nameof(Card));
        }
    }
}
