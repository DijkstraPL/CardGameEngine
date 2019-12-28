using CardGame_Game.Cards.Enums;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Cards.Interfaces
{
    public interface ICard
    {
        int Id { get; }
        string Name { get;  }
        int Cost { get; }
        ICardType Type { get; }
        ISubType SubType { get; }
        CardColor Color { get; }
        Rarity Rarity { get; }
        string Description { get; }
        string Quotation { get; }

        void Play(IGame game, IPlayer player);
    }
}
