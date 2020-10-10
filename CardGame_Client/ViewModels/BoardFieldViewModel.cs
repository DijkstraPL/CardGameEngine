using CardGame_Client.Events;
using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

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

        public ICommand FieldSelectedCommand { get;  }

        public event EventHandler<FieldSelectorEventArgs> FieldClicked;

        private FieldData _field;
        private readonly bool _isEnemyField;
        private readonly ICardGameManagement _cardGameManagement;

        public BoardFieldViewModel(FieldData field, bool isEnemyField, ICardGameManagement cardGameManagement)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _isEnemyField = isEnemyField;
            _cardGameManagement = cardGameManagement ?? throw new ArgumentNullException(nameof(cardGameManagement));
            ContainsCard = _field.UnitCard != null;
            if (ContainsCard)
            {
                Name = _field.UnitCard.Name;
                FinalAttack = _field.UnitCard.FinalAttack;
                Cooldown = _field.UnitCard.Cooldown;
                FinalHealth = _field.UnitCard.FinalHealth;
            }

            FieldClicked += _cardGameManagement.OnFieldSelected;
            
            FieldSelectedCommand = new DelegateCommand(() =>
            {
                FieldClicked?.Invoke(this, new FieldSelectorEventArgs(_field, _isEnemyField));
            });
        }
    }
}
