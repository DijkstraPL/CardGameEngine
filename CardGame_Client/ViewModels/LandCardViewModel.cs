using CardGame_Data.GameData;
using Prism.Mvvm;
using System;

namespace CardGame_Client.ViewModels
{
    public class LandCardViewModel : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private int? _cooldown;
        public int? Cooldown
        {
            get => _cooldown;
            set => SetProperty(ref _cooldown, value);
        }


        private CardData _landCard;

        public LandCardViewModel(CardData landCard)
        {
            _landCard = landCard ?? throw new ArgumentNullException(nameof(landCard));
            Name = _landCard.Name;
            Cooldown = _landCard.Cooldown;
        }
    }
}
