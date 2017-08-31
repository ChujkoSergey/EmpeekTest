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
        [HttpPost]
        [Route("")]
        public IEnumerable<ItemsInfoMessage> TestMethod(InfoRequestMessage request)
        {
            return ((ItemsContext)MainContext.Instance.Items).GetItemInfoPage(request.Page, request.Count);
        }

        [HttpPost]
        [Route("pages")]
        public ResultMessage GetCountOfPages(InfoRequestMessage request)
        {
            try
            {
                var count = MainContext.Instance.Items.GetAll().Count();
                var temp = count % request.Count;
                return new ResultMessage()
                {
                    ResultCode = (temp != 0) ? ((count / request.Count) + 1) : (count / request.Count)
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

        [HttpPost]
        [Route("edit")]
        public ResultMessage EditItem(ItemsInfoMessage editedItem)
        {
            try
            {
                var type = MainContext.Instance.Type.GetBy(x => x.Name == editedItem.Type)?.ToList()[0];
                if (type == null)
                {
                    if (!MainContext.Instance.Type.Insert(new Model.Models.Type() { Name = editedItem.Type }))
                    {
                        return new ResultMessage()
                        {
                            ResultCode = 0,
                            Message = "Can't insert new type"
                        };
                    }
                    type = MainContext.Instance.Type.GetBy(x => x.Name == editedItem.Type)?.ToList()[0];
                }
                if (MainContext.Instance.Items.Update(new Items() { Name = editedItem.Name, TypeId = type.Id }, x => x.Id == editedItem.Id))
                {
                    return new ResultMessage()
                    {
                        ResultCode = 1,
                        Message = "Item was edited"
                    };
                }
                return new ResultMessage()
                {
                    ResultCode = 0,
                    Message = "Can't edit this item"
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

        [HttpPost]
        [Route("delete")]
        public ResultMessage DeleteItem(DeleteItemMessage deleteRequest)
        {
            try
            {
                if(MainContext.Instance.Items.Delete(x => x.Id == deleteRequest.Id))
                {
                    return new ResultMessage()
                    {
                        ResultCode = 1,
                        Message = "Item was deleted"
                    };
                }
                return new ResultMessage()
                {
                    ResultCode = 0,
                    Message = "Can't delete this item"
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
