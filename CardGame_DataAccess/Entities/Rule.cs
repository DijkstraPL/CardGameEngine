using Data = CardGame_Data.Data;

namespace CardGame_DataAccess.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string Effect { get; set; }
        public string Description { get; set; }

        public static implicit operator Data.Rule(Rule rule)
        {
            if (rule == null)
                return null;
            return new Data.Rule
            {
                Effect = rule.Effect,
                Description = rule.Description
            };
        }
    }
}
