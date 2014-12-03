using System.Runtime.Caching;
using BotWars.Models;

namespace BotWars.Services
{
    public class BotManager : IBotManager
    {

        private readonly ObjectCache _cache = MemoryCache.Default;

        public void AddBot(Bot bot)
        {
            var cacheKey = bot.Name;
            _cache.GetOrAdd(cacheKey, () => bot, new CacheItemPolicy());
        }

        public string Move()
        {
            return Moves.CALL.ToString();
        }
    }
}
