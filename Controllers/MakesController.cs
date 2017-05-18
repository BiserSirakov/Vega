namespace Vega.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;

    using Data;
    using Data.Models;
    using Resources;

    public class MakesController : BaseController
    {
        private readonly VegaDbContext _context;

        public MakesController(IMapper mapper, VegaDbContext context)
            : base(mapper)
        {
            _context = context;
        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes.Include(x => x.Models).ToListAsync();

            return Mapper.Map<List<MakeResource>>(makes);
        }
    }
}
