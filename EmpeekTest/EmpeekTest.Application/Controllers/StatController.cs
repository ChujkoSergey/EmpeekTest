using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpeekTest.Model.Messages;
using EmpeekTest.Model.Models;
using EmpeekTest.Model.Contexts;

namespace EmpeekTest.Application.Controllers
{
    [RoutePrefix("api/stat")]
    public class StatController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IEnumerable<StatMessage> GetTypeStats(InfoRequestMessage request)
        {
            return ((TypeContext)(MainContext.Instance.Type)).GetTypeStats(request);
        }
    }
}
