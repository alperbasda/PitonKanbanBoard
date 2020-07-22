using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Entities.Dtos.Post.UserCalender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserCalenderService _calenderService;
        public HomeController(IHttpContextAccessor httpContextAccessor, IUserCalenderService calenderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _calenderService = calenderService;
        }

        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Events()
        {
            return Json(_calenderService.UserEvents());
        }

        public JsonResult InsertEvent(PostUserCalenderModel model)
        {
            return Json(_calenderService.CreateEvent(model));
        }

        public JsonResult UpdateEvent(PostUserCalenderUpdateTypeModel model)
        {
            return Json(_calenderService.UpdateEvent(model));
        }

        public JsonResult DeleteEvent(int id)
        {
            return Json(_calenderService.DeleteEvent(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
