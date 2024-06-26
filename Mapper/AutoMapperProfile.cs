using AutoMapper;
using Dtos;
using Models;

namespace Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieDto,MovieModel>().ReverseMap();
            CreateMap<UserDto, UserModel>().ReverseMap();
        }
    }
}
