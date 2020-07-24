using Core.Utilities.Results;
using Entities.Dtos.Post.UserCalender;

namespace Business.Abstract
{
    public interface IUserCalenderService
    {
        Result UserEvents(int? userId=null);

        Result CreateEvent(PostUserCalenderModel model, int? userId = null);

        Result UpdateEvent(PostUserCalenderUpdateTypeModel model, int? userId = null);

        Result DeleteEvent(int id, int? userId = null);
    }
}