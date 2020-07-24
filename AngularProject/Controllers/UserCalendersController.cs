using Business.Abstract;
using Entities.Dtos.Post.UserCalender;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCalendersController : ControllerBase
    {
        private IUserCalenderService _calenderService;

        public UserCalendersController(IUserCalenderService calenderService)
        {
            _calenderService = calenderService;
        }

        [HttpGet("events")]
        public ActionResult Events(int userId)
        {
            var res = _calenderService.UserEvents(userId);
            return Ok(res);
        }

        [HttpGet("insert")]
        public ActionResult InsertEvent(int userId,string description,int recordType)
        {
            PostUserCalenderModel m= new PostUserCalenderModel();
            m.Description = description;
            m.RecordType = (RecordType)recordType;
            return Ok(_calenderService.CreateEvent(m, userId));
        }

        [HttpPost("update")]
        public ActionResult UpdateEvent(PostUserCalenderUpdateTypeModel model, int userId)
        {
            return Ok(_calenderService.UpdateEvent(model, userId));
        }

        [HttpGet("delete")]
        public ActionResult DeleteEvent(int id, int userId)
        {
            return Ok(_calenderService.DeleteEvent(id, userId));
        }
    }
}