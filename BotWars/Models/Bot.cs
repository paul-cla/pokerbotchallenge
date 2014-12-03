namespace BotWars.Models
{
    public class Bot
    {
        private string _nextMove;

        public Bot(string name, int startingChipCount, int handLimit)
        {
            Name = name;
            ChipCount = startingChipCount;
            HandLimit = handLimit;
        }

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
