using Data = CardGame_Data.Data;

namespace CardGame_DataAccess.Entities
{
    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static implicit operator Data.CardType(CardType cardType)
            => new Data.CardType
            {
                Name = cardType.Name
            };
    }
}
