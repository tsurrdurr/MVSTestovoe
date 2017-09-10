using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StackWebAPI.Controllers
{
    public class MainController : ApiController
    {
        private MyStack myStack = new MyStack( new string[]
            {
            "test",
            "123"
            }
        );

        [HttpGet]
        [ActionName("Peek")]
        public IHttpActionResult PeekStack()
        {
            try
            {
                var value = myStack.Peek();
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
                var value = myStack.Pop();
                return Json(value);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ActionName("Push")]
        public IHttpActionResult PushStack(string text)
        {
            try
            {
                if (text == null) throw new ArgumentNullException();
                myStack.Push(text);
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
                var value = myStack.GetSize();
                return Json(value);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
