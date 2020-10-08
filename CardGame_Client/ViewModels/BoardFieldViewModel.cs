using CardGame_Data.GameData;
using Prism.Mvvm;
using System;

namespace CardGame_Client.ViewModels
{
    public class BoardFieldViewModel : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int? _finalAttack;
        public int? FinalAttack
        {
            get => _finalAttack;
            set => SetProperty(ref _finalAttack, value);
        }

        private int? _cooldown;
        public int? Cooldown
        {
            get => _cooldown;
            set => SetProperty(ref _cooldown, value);
        }

        private int? _finalHealth;
        public int? FinalHealth
        {
            get => _finalHealth;
            set => SetProperty(ref _finalHealth, value);
        }

        private bool _containsCard;
        public bool ContainsCard
        {
            get => _containsCard;
            set => SetProperty(ref _containsCard, value);
        }

        private FieldData _field;

        public BoardFieldViewModel(FieldData field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));

            ContainsCard = _field.UnitCard != null;
            if (ContainsCard)
            {
                Name = _field.UnitCard.Name;
                FinalAttack = _field.UnitCard.FinalAttack;
                Cooldown = _field.UnitCard.Cooldown;
                FinalHealth = _field.UnitCard.FinalHealth;
            }
        }
    }
}
