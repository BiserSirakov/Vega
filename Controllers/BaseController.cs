namespace Vega.Controllers
{
    using AutoMapper;
    
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; set; }
    }
}