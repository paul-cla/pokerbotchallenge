namespace BotWars.Models
{
    public class Moves
    {
        private string _move;

        public Moves(string move)
        {
            _move = move;
        }

        public string Move
        {
            get { return _move; }
        }
    }
}