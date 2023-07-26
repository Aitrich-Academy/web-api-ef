using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDto, User>().ReverseMap();
			CreateMap<UserProfileDto, User>().ReverseMap();
			CreateMap<InterviewDto, Interview>().ReverseMap();
			CreateMap<InterviewDto, Interview>()
				 .ForMember(dest => dest.Time, opt => opt.MapFrom(src =>
				  TimeSpan.Parse(src.Time)));
				
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<JobDto,Job>().ReverseMap();
			CreateMap<CompanyMemberDto, User>().ReverseMap();
		}
	}
}
