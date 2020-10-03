using Data = CardGame_Data.Data;

namespace CardGame_DataAccess.Entities
{
    public class Subtype
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator Data.Subtype(Subtype subtype)
        {
            if (subtype == null)
                return null;
            return new Data.Subtype
            {
                Name = subtype.Name
            };
        }
    }
}
