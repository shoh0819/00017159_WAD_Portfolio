using _00017159_WAD_Portfolio.DTOs;
using _00017159_WAD_Portfolio.Models;
using AutoMapper;

namespace _00017159_WAD_Portfolio.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<User, UserDto>();
        }
    }
}
