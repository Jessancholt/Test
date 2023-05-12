using AutoMapper;
using Test.DataAccess.Entities;
using Test.WebAPI.Models.ApiRequests;
using Test.WebAPI.Models.ApiResponses;

namespace Test.WebAPI.Infrastructure.MappingProfiles
{
    public class PostModelMapProfile : Profile
    {
        public PostModelMapProfile()
        {
            CreateMap<Post, PostResponse>();

            CreateMap<PostRequest, Post>();
        }
    }
}
