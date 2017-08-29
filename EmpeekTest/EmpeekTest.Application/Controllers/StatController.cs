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
        [HttpGet]
        [Route("")]
        public IEnumerable<StatMessage> GetTypeStats()
        {
            return ((TypeContext)(MainContext.Instance.Type)).GetTypeStats();
        }
    }
}
