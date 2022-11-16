using AebTestProject.Models.Request;
using AebTestProject.Models.Response;
using AutoMapper;

namespace AebTestProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateTask, Models.Task>();
            CreateMap<Models.Task, TaskResponseModel>();
            CreateMap<UpdateTask, Models.Task>();
        }
    }
}
