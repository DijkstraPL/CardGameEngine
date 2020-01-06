using CardGame_Data.Entities.Enums;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;

namespace CardGame_Game.Cards.Interfaces
{
    public interface ICard
    {
        int Id { get; }
        int Number { get;  }
        string Name { get; }
        string Description { get; }
        string Trait { get;  }
        ICollection<IRule> Rules { get; }
        string Flavour { get;  }
        Kind Kind { get;  }
        int CardTypeId { get;  }
        ICardType CardType { get; }
        int? SubTypeId { get;  }
        ISubtype SubType { get; }
        int? Attack { get;  }
        int? Cooldown { get; }
        int? Health { get;  }
        int? CostGreen { get;  }
        int? CostWhite { get;  }
        int? CostRed { get; }
        Rarity Rarity { get; }
        CardColor Color { get;  }
        bool IsPublic { get; }
        int Set { get; }

        void Play(IGame game, IPlayer player);
    }
}
