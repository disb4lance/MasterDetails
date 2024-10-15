using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Master_n_Details
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Master, MasterDto>();

            CreateMap<Detail, DetailDto>();

            CreateMap<MasterForCreatingDto, Master>();

            CreateMap<DetailForCreatingDto, Detail>();

            CreateMap<DetailForUpdateDto, Detail>().ReverseMap();

            CreateMap<MasterForUpdateDto, Master>();

        }
    }
}
