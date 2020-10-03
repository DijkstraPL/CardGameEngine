using Data = CardGame_Data.Data;

namespace CardGame_DataAccess.Entities
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static implicit operator Data.Set(Set set)
            => new Data.Set
            {
                Name = set.Name
            };
    }
}
