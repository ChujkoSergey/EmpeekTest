using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpeekTest.Model.Models;
using EmpeekTest.Model.Contexts;
using EmpeekTest.Model.Messages;

namespace EmpeekTest.Application.Controllers
{
    [RoutePrefix("api/main")]
    public class MainController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<Items> TestMethod()
        {
            return MainContext.Instance.Items.GetAll();
        }

        [HttpPost]
        [Route("add")]
        public ResultMessage AddItem(NewItemMessage newItem)
        {
            try
            {
                var type = MainContext.Instance.Type.GetBy(x => x.Name == newItem.Type)?.ToList()[0];
                if (type == null)
                {
                    if(!MainContext.Instance.Type.Insert(new Model.Models.Type() { Name = newItem.Type}))
                    {
                        return new ResultMessage()
                        {
                            ResultCode = 0,
                            Message = "Can't insert new type"
                        };
                    }
                    type = MainContext.Instance.Type.GetBy(x => x.Name == newItem.Type)?.ToList()[0];
                }
                if (MainContext.Instance.Items.Insert(new Items() { Name = newItem.Name, TypeId = type.Id }))
                {
                    return new ResultMessage()
                    {
                        ResultCode = 1,
                        Message = "New item succesfully added"
                    };
                }
                return new ResultMessage()
                {
                    ResultCode = 0,
                    Message = "Can't insert new item"
                };
            }
            catch(Exception e)
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
