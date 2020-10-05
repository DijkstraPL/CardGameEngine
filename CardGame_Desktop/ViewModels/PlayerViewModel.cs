using CardGame_Game.BoardTable;
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

        public ObservableCollection<GameCard> Hand { get; }
        public string Name => Player.Name;
        public int Energy => Player.Energy;
        public int? Morale => (Player as BluePlayer)?.Morale;
        public int? HitPoints => Player.FinalHealth;
        public BoardSideViewModel BoardSide { get;  }

        public int DeckCardCount => Player.Deck.Count;
        public int LandDeckCardCount => Player.LandDeck.Count;

        public PlayerViewModel(IPlayer player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Hand = new ObservableCollection<GameCard>(Player.Hand);
            BoardSide = new BoardSideViewModel(Player.BoardSide, Player);
        }

        public void RefreshHand()
        {
            Hand.Clear();
            foreach (var card in Player.Hand)
                Hand.Add(card);
            OnPropertyChanged(nameof(Hand));
            OnPropertyChanged(nameof(Morale));
            OnPropertyChanged(nameof(DeckCardCount));
            OnPropertyChanged(nameof(LandDeckCardCount));
        }

        internal void RefreshEnergy()
        {
            OnPropertyChanged(nameof(Energy));
            OnPropertyChanged(nameof(Morale));
        }

        internal void RefreshHitPoints()
        {
            OnPropertyChanged(nameof(HitPoints));
            OnPropertyChanged(nameof(Morale));
        }
    }
}
