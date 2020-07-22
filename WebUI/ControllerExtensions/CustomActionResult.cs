using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebUI.ControllerExtensions
{
    public class CustomActionResult : ActionResult
    {
        public ActionResult BaseResult { get; }

        private readonly string _message;

        private readonly string _type;

        public CustomActionResult(ActionResult redirectBaseResult, string message, string type)
        {
            BaseResult = redirectBaseResult;
            _message = message;
            _type = type;
        }

        public override void ExecuteResult(ActionContext context)
        {
            
            var tm= context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
            var temps = tm.GetTempData(context.HttpContext);
            temps.Add(_type,_message);
            
            BaseResult.ExecuteResult(context);
        }

    }
}