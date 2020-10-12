using CardGame_Client.Events;
using CardGame_Client.Services.Interfaces;
using CardGame_Client.ViewModels.Interfaces;
using CardGame_Data.GameData;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace CardGame_Client.ViewModels
{
    public class BoardFieldViewModel : BindableBase, IPosition
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
        public FieldData Field { get; }

        public ICommand FieldSelectedCommand { get;  }

        public event EventHandler<FieldSelectorEventArgs> FieldClicked;

        private readonly bool _isEnemyField;
        private readonly ICardGameManagement _cardGameManagement;

        public BoardFieldViewModel(FieldData field, bool isEnemyField, ICardGameManagement cardGameManagement)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            _isEnemyField = isEnemyField;
            _cardGameManagement = cardGameManagement ?? throw new ArgumentNullException(nameof(cardGameManagement));
            ContainsCard = Field.UnitCard != null;
            if (ContainsCard)
            {
                Name = Field.UnitCard.Name;
                FinalAttack = Field.UnitCard.FinalAttack;
                Cooldown = Field.UnitCard.Cooldown;
                FinalHealth = Field.UnitCard.FinalHealth;
            }

            FieldClicked += _cardGameManagement.OnFieldSelected;
            
            FieldSelectedCommand = new DelegateCommand(() =>
            {
                FieldClicked?.Invoke(this, new FieldSelectorEventArgs(Field, _isEnemyField));
            });
        }
    }
}
