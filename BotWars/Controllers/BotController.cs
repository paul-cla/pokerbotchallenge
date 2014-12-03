using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BotWars.Models;

namespace BotWars.Controllers
{
    public class BotController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Start(CreateBot createBot)
        {
            return new HttpResponseMessage((HttpStatusCode) 418);
        }

    }
}
