namespace BotWars.Models
{
    public class Bot
    {
        public Bot(string name, int startingChipCount, int handLimit)
        {
            Name = name;
            ChipCount = startingChipCount;
            HandLimit = handLimit;
        }

        public string Name { get; set; }
        public int ChipCount { get; set; }
        public int HandLimit { get; set; }

        public Moves Move()
        {
            return Moves.CALL;
        }
    }
}
