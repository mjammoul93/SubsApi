using AutoMapper;
using SubsApi.DTOs;
using SubsApi.Models;

namespace SubsApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AppUser, MemberDTO>();
        }
    }
}
