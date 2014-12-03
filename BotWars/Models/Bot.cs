namespace BotWars.Models
{
    public class Bot
    {
        private string _nextMove;

        public Bot(string name, int startingChipCount, int handLimit, int bigBlind, int smallBlind)
        {
            Name = name;
            ChipCount = startingChipCount;
            HandLimit = handLimit;
            BigBlind = bigBlind;
            SmallBlind = smallBlind;
        }

        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public string Name { get; set; }
        public int ChipCount { get; set; }
        public int HandLimit { get; set; }

        public string NextMove
        {
            set { _nextMove = value; }
        }

        public string Move()
        {
            return _nextMove ?? "CALL";
        }
    }
}
