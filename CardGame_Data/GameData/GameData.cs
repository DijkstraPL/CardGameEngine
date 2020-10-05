using System;
using System.Text;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class GameData
    {
        public int TurnCounter { get; set; }
        public BoardData Board { get; set; }

        public PlayerData CurrentPlayer { get; set; }
        public PlayerData NextPlayer { get; set; }
        public bool IsControllingCurrentPlayer { get; set; }
    }
}
