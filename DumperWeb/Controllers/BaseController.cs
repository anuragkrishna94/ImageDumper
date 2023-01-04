using DumperApplicationCore.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DumperWeb.Controllers
{
    public class BaseController : Controller
    {
        private readonly DumpAndFetch _manager;
        public BaseController(DumpAndFetch manager)
        {
            _manager = manager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
