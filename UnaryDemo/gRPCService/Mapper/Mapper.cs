using AutoMapper;

namespace gRPCService.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<models.ProductModels.ProductModel,models.ProductModels.Dto.ProductModelDto>();
        }
    }
}
