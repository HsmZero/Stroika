using Stroika.DAL.Entity;

namespace Stroika.Services.Interface
{
    public interface ILookupsService
    {
        Task<List<Nationality>> GetAllNationalities();
    }
}