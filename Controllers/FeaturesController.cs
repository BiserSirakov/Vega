namespace Vega.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;

    using Data;
    using Resources;

    public class FeaturesController : BaseController
    {
        private readonly VegaDbContext _context;

        public FeaturesController(IMapper mapper, VegaDbContext context)
            : base(mapper)
        {
            _context = context;
        }

        [HttpGet("api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();

            return Mapper.Map<List<KeyValuePairResource>>(features);
        }
    }
}
