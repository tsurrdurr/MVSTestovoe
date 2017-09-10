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
        private const string emptyStack = "Стек пуст";

        [HttpPost]
        [ResponseType(typeof(string))]
        [ActionName("Push")]
        public IHttpActionResult PushStack([FromBody] string text)
        {
            try
            {
                if (text == null) throw new ArgumentNullException();
                commonStack.Push(text);
                return Content(HttpStatusCode.OK, "Успешно");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
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
            catch (Exception ex)
            {
                if (ex is InvalidOperationException) return Content(HttpStatusCode.BadRequest, emptyStack);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("Peek")]
        public IHttpActionResult PeekStack()
        {
            try
            {
                var value = commonStack.Peek();
                return Json(value);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException) return Content(HttpStatusCode.BadRequest, emptyStack);
                return Content(HttpStatusCode.BadRequest, ex.Message);
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
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
