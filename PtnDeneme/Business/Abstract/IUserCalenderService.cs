using Core.Utilities.Results;
using Entities.Dtos.Post.UserCalender;

namespace Business.Abstract
{
    public interface IUserCalenderService
    {
        Result UserEvents();

        Result CreateEvent(PostUserCalenderModel model);

        Result UpdateEvent(PostUserCalenderUpdateTypeModel model);

        Result DeleteEvent(int id);
    }
}