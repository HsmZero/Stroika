using Microsoft.AspNetCore.Mvc;
using Stroika.DAL.Entity;
using Stroika.Models.Students;
using Stroika.Services.Interface;

namespace Stroika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalitiesController : ControllerBase
    {
        ILookupsService _lookupsService;
        public NationalitiesController(ILookupsService lookupsService)
        {
            _lookupsService = lookupsService;
        }
        [HttpGet(Name = "Nationalities")]
        public async Task<List<Nationality>> Nationalities()
        {
            var data = await _lookupsService.GetAllNationalities();
            return data;

        }
    }
}
