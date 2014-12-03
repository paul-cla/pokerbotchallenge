using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BotWars.Models;
using BotWars.Services;
using log4net;

namespace BotWars.Controllers
{
    public class BotController : ApiController
    {
        public BotController(IBotManager botManager, ILog log)
        {
            _botManager = botManager;
            _log = log;
        }

        private readonly IBotManager _botManager;
        private readonly ILog _log;

        [HttpPost]
        public HttpResponseMessage Start(FormDataCollection form)
        {
            var createBotForm = new CreateBotForm
                {
                    HandLimit = form.Get("HAND_LIMIT"),
                    OpponentName = form.Get("OPPONENT_NAME"),
                    StartingChipCount = form.Get("STARTING_CHIP_COUNT"),
                    BigBlind = form.Get("BIG_BLIND"),
                    SmallBlind = form.Get("SMALL_BLIND")
                };

            foreach (var formItem in form)
            {
                _log.Info(formItem.Key + ":" + formItem.Value);
            }


            _log.Info(createBotForm.ToString());

            var bot = new Bot(
                createBotForm.OpponentName, 
                Convert.ToInt32(createBotForm.StartingChipCount),
                Convert.ToInt32(createBotForm.HandLimit), 
                Convert.ToInt32(createBotForm.BigBlind),
                Convert.ToInt32(createBotForm.SmallBlind)
                );
            _botManager.AddBot(bot);
            return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created
                };
        }

        [HttpGet]
        public HttpResponseMessage Move()
        {
            return new HttpResponseMessage
                {
                    Content = new StringContent(_botManager.Move())
                };
        }

        [HttpPost]
        public HttpResponseMessage Update(UpdateForm updateForm)
        {
            _log.Info(updateForm);
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
