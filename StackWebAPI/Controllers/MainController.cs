using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace StackWebAPI.Controllers
{
    public class MainController : ApiController
    {
        public MainController()
        {
            
        }

        private CommonStack commonStack = CommonStack.Instance;

        [HttpGet]
        [ActionName("Peek")]
        public IHttpActionResult PeekStack()
        {
            try
            {
                var value = commonStack.Peek();
                return Json(value);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("Pop")]
        public IHttpActionResult PopStack()
        {
            try
            {
                var value = commonStack.Pop();
                return Json(value);
            }
            catch(Exception ex)
            {
                if (ex is InvalidOperationException) return BadRequest("Стек пуст");
                return BadRequest();
            }
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [ActionName("Push")]
        public IHttpActionResult PushStack([FromBody] string text)
        {
            try
            {
                if (text == null) throw new ArgumentNullException();
                commonStack.Push(text);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("Size")]
        public IHttpActionResult SizeOfStack()
        {
            try
            {
                var value = commonStack.GetSize();
                return Json(value);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
