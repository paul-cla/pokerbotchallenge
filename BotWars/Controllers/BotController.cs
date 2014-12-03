using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BotWars.Models;
using BotWars.Services;

namespace BotWars.Controllers
{
    public class BotController : ApiController
    {
        public BotController(IBotManager botManager)
        {
            _botManager = botManager;
        }

        private readonly IBotManager _botManager;

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Start(CreateBotForm createBotForm)
        {

            var bot = new Bot(createBotForm.OPPONENT_NAME, Convert.ToInt32(createBotForm.STARTING_CHIP_COUNT), Convert.ToInt32(createBotForm.HAND_LIMIT));
            _botManager.AddBot(bot);
            return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created
                };
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Move()
        {
            return new HttpResponseMessage
                {
                    Content = new StringContent(_botManager.Move())
                };
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Update(UpdateForm updateForm)
        {
            if (updateForm != null && updateForm.COMMAND != null)
            {
                if (updateForm.DATA == null)
                {
                    updateForm.DATA = string.Empty;
                }

                _botManager.Update(updateForm.COMMAND, updateForm.DATA);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
