using AutoMapper;
using Data.Entities;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ServiceCreateModel, Data.Entities.Services>();
            CreateMap<Data.Entities.Services, ServiceModel>();
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}
