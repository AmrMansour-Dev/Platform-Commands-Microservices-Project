using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>>GetPlatforms()
        {
            Console.WriteLine("Getting Platforms...");

            var Platforms = _repository.GetAllPlatforms();

            //Mapping to PlatformReadDTO From Platforms(Platform Model)
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(Platforms));
        }
    }
}
