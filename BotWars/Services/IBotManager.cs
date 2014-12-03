using BotWars.Models;

namespace BotWars.Services
{
    public interface IBotManager
    {
        void AddBot(Bot bot);
        string Move();
        void Update(string command, string data);
    }
}