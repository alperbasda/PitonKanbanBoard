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

        [SecuredOperation]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public Result UserEvents()
        {
            var id = AuthenticateHelper.AuthenticateUserId();

            return new Result
            {
                Message = "Görev Listesi",
                Success = true,
                Data = _mapper.Map<List<GetUserCalenderModel>>(_calenderDal.GetList(s => s.UserId == id))
            };
        }

        [SecuredOperation]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public Result CreateEvent(PostUserCalenderModel model)
        {
            var userId = AuthenticateHelper.AuthenticateUserId();
            var entity = _mapper.Map<UserCalender>(model);
            entity.UserId = userId;

            var turnData= _calenderDal.Add(entity);
            return new Result
            {
                Success = true,
                Message = "Kayıt Başarılı",
                Data = _mapper.Map<GetUserCalenderModel>(turnData)
            };
        }

        [SecuredOperation]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public Result UpdateEvent(PostUserCalenderUpdateTypeModel model)
        {
            var entity = _calenderDal.Get(s => s.Id == model.Id);
            if (entity == null)
                return new Result
                {
                    Message = "Veri Bulunamadı",
                    Success = false
                };

            var userId = AuthenticateHelper.AuthenticateUserId();
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

        [SecuredOperation]
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        public Result DeleteEvent(int id)
        {
            var entity = _calenderDal.Get(s => s.Id == id);
            if(entity== null)
                return new Result
                {
                    Message = "Veri Bulunamadı",
                    Success = false
                };

            var userId = AuthenticateHelper.AuthenticateUserId();
            if(entity.UserId != userId)
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