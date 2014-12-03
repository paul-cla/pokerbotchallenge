using System.Runtime.Caching;
using BotWars.Models;

namespace BotWars.Services
{
    public class BotManager : IBotManager
    {
        private readonly ObjectCache _cache = MemoryCache.Default;

        public Bot Bot
        {
            get
            {
                var bot = (Bot)_cache.Get("Bot");
                return bot;
            }
        }

        public void AddBot(Bot bot)
        {
            _cache.Set("Bot", bot, new CacheItemPolicy());
        }

        public string Move()
        {
            var bot = (Bot)_cache.Get("Bot");
            if (bot == null)
            {
                throw new BotNotCreatedException();
            }
            return bot.Move();
        }

        public void Update(string command, string data)
        {
            var bot = (Bot)_cache.Get("Bot");

            if (bot == null)
            {
                throw new BotNotCreatedException();
            }

            if (command == "CARD")
            {
                if (data == "A")
                {
                    bot.NextMove = "BET:" + bot.ChipCount;
                    bot.ChipCount -= bot.ChipCount;
                }
                if (data == "K")
                {
                    bot.NextMove = "BET:" + bot.ChipCount / 2;
                    bot.ChipCount -= bot.ChipCount / 2;
                }
                if (data == "Q")
                {
                    bot.NextMove = "BET:" + bot.ChipCount / 3;
                    bot.ChipCount -= bot.ChipCount / 3;
                }
                if (data == "J")
                {
                    bot.NextMove = "BET:" + bot.ChipCount / 4;
                    bot.ChipCount -= bot.ChipCount / 4;
                }
                if (data == "6")
                {
                    bot.NextMove = "BET";
                    bot.ChipCount -= 1;
                }

                if (data == "2" ||
                    data == "3" ||
                    data == "4" ||
                    data == "5")
                {
                    bot.NextMove = "FOLD";
                }
            }

            if (command == "RECEIVE_CHIPS")
            {
                bot.ChipCount += int.Parse(data);
            }

            if (command == "POST_BLIND")
            {
                bot.ChipCount -= 1;
            }

            _cache.Set("Bot", bot, new CacheItemPolicy());
        }

    }
}
