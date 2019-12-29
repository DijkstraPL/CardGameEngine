using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Data.Entities
{
    public class CardRule
    {
        public int CardId { get; set; }
        public int RuleId { get; set; }
        public Card Card { get; set; }
        public Rule Rule { get; set; }
    }
}
