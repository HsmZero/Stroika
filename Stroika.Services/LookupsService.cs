using Microsoft.EntityFrameworkCore;
using Stroika.DAL;
using Stroika.DAL.Entity;
using Stroika.Models.Students;
using Stroika.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroika.Services
{
    public class LookupsService : ILookupsService
    {
        StroikaDBContext _dbContext;
        public LookupsService(StroikaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Nationality>> GetAllNationalities()
        {
            var data = await _dbContext.Nationalities.ToListAsync();

            return data;
        }
    }
}
