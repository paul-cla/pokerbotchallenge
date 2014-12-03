using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotWars.Models;

namespace BotWars.Controllers
{
    public class BotController : ApiController
    {
        private Bot _bot;
        [HttpPost]
        public HttpResponseMessage Start(CreateBotForm createBotForm)
        {
            _bot = new Bot(createBotForm.OPPONENT_NAME, Convert.ToInt32(createBotForm.STARTING_CHIP_COUNT), Convert.ToInt32(createBotForm.HAND_LIMIT));
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        
        [HttpGet]
        public string Move()
        {
            return Moves.CALL.ToString();
        }
        
        [HttpPost]
        public HttpResponseMessage Update()
        {
            return new HttpResponseMessage((HttpStatusCode) 418);
        }

    }
}
