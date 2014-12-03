using System;

namespace BotWars.Services
{
    public class BotNotCreatedException : Exception
    {
        public override string Message
        {
            get { return "You've not setup the bot"; }
        }
    }
}