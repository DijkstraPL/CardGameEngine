using CardGame_Game.Cards;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CardGame_Desktop.ViewModels
{
    public class PlayerViewModel : Notifier
    {
        public IPlayer Player { get; }

        public ObservableCollection<GameCard> Hand { get; private set; }
        public string Name => Player.Name;
        public int Energy => Player.Energy;

        public PlayerViewModel(IPlayer player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Hand = new ObservableCollection<GameCard>(Player.Hand);
        }

        public void RefreshHand()
        {
            Hand.Clear();
            foreach (var card in Player.Hand)
                Hand.Add(card);
            OnPropertyChanged(nameof(Hand));
        }

        internal void RefreshEnergy()
        {
            OnPropertyChanged(nameof(Energy));
        }
    }
}
