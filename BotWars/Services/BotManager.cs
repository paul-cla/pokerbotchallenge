using System.Runtime.Caching;
using BotWars.Models;
using log4net;

namespace BotWars.Services
{
    public class BotManager : IBotManager
    {
        private readonly ILog _log;
        private readonly ObjectCache _cache = MemoryCache.Default;


        public BotManager(ILog log)
        {
            _log = log;
        }

        public BotManager()
        {
            
        }

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
            if (_log != null) _log.Info("Created " + bot.Name);
            _cache.Set("Bot", bot, new CacheItemPolicy());
        }

        public string Move()
        {
            var bot = (Bot)_cache.Get("Bot");
            if (bot == null)
            {
                throw new BotNotCreatedException();
            }
            
            var move = bot.Move();
            if (_log != null) _log.Info("Move " + move);
            return move;
        }

        public void Update(string command, string data)
        {
            var bot = (Bot)_cache.Get("Bot");

            if (_log != null) _log.Info(bot.Name + " " + command + " "+ data);

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
                
                if (data == "8" ||
                    data == "9" ||
                    data == "10")
                {
                    bot.NextMove = "CALL";
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
