using System.Collections.Generic;
using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Helpers;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Get.UserCalender;
using Entities.Dtos.Post.UserCalender;

namespace Business.Concrete
{
    public class UserCalenderManager : IUserCalenderService
    {
        private IUserCalenderDal _calenderDal;
        private IMapper _mapper;

        public UserCalenderManager(IUserCalenderDal calenderDal, IMapper mapper)
        {
            _calenderDal = calenderDal;
            _mapper = mapper;
        }


        [ExceptionLogAspect(typeof(DatabaseLogger))]

        public Result UserEvents(int? userId = null)
        {
            var id = userId ?? AuthenticateHelper.AuthenticateUserId();

            return new Result
            {
                Message = "Görev Listesi",
                Success = true,
                Data = _mapper.Map<List<GetUserCalenderModel>>(_calenderDal.GetList(s => s.UserId == id))
            };
        }


        [ExceptionLogAspect(typeof(DatabaseLogger))]

        public Result CreateEvent(PostUserCalenderModel model, int? userId = null)
        {
            userId = userId ?? AuthenticateHelper.AuthenticateUserId();
            var entity = _mapper.Map<UserCalender>(model);
            entity.UserId = (int)userId;

            var turnData = _calenderDal.Add(entity);
            return new Result
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Data = _mapper.Map<GetUserCalenderModel>(turnData)
            };
        }


        [ExceptionLogAspect(typeof(DatabaseLogger))]

        public Result UpdateEvent(PostUserCalenderUpdateTypeModel model, int? userId = null)
        {
            var entity = _calenderDal.Get(s => s.Id == model.Id);
            if (entity == null)
                return new Result
                {
                    Message = "Veri Bulunamadı",
                    Success = false
                };

            userId = userId ?? AuthenticateHelper.AuthenticateUserId();
            if (entity.UserId != userId)
                return new Result
                {
                    Message = "Bu Veriyi Düzenleme Yetkiniz Yok",
                    Success = false
                };

            entity.RecordType = model.RecordType;
            _calenderDal.Update(entity);

            return new Result
            {
                Message = "Veri Düzenlendi",
                Success = true
            };
        }


        [ExceptionLogAspect(typeof(DatabaseLogger))]

        public Result DeleteEvent(int id, int? userId = null)
        {
            var entity = _calenderDal.Get(s => s.Id == id);
            if (entity == null)
                return new Result
                {
                    Message = "Veri Bulunamadı",
                    Success = false
                };

            userId = userId ?? AuthenticateHelper.AuthenticateUserId();
            if (entity.UserId != userId)
                return new Result
                {
                    Message = "Bu Veriyi Silme Yetkiniz Yok",
                    Success = false
                };

            _calenderDal.Delete(entity);
            return new Result
            {
                Message = "Veri Silindi",
                Success = true
            };
        }
    }
}