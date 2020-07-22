using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.Get.UserCalender;
using Entities.Dtos.Post.UserCalender;

namespace Business.AutoMapperProfile
{
  public  class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<PostUserCalenderModel,UserCalender>();
            CreateMap<UserCalender, GetUserCalenderModel>()
                .ForMember(s=>s.RecordTypeInt,w=>w.Ignore());

        }
    }
}
