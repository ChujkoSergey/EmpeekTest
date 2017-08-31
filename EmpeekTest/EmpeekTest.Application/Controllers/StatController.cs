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


        [HttpPost]
        [Route("pages")]
        public ResultMessage GetCountOfPages(InfoRequestMessage request)
        {
            try
            {
                var count = MainContext.Instance.Type.GetAll().Count();
                var temp = count % request.Count;
                return new ResultMessage()
                {
                    ResultCode = (temp != 0) ? ((count / request.Count) + 1) : (count / request.Count)
                };
            }
            catch (Exception e)
            {
                return new ResultMessage()
                {
                    ResultCode = -1,
                    Message = $"Internal server error: {e.Message}"
                };
            }
        }
    }
}
