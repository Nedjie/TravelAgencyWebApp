using AutoMapper;

namespace TravelAgencyWebApp.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
