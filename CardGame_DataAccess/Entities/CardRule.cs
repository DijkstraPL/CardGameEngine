﻿namespace CardGame_DataAccess.Entities
{
    public class CardRule
    {
        public int CardId { get; set; }
        public int RuleId { get; set; }
        public Card Card { get; set; }
        public Rule Rule { get; set; }
    }
}
