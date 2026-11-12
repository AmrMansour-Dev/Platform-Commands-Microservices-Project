using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System.Net.WebSockets;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepository repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>>GetPlatforms()
        {
            Console.WriteLine("Getting Platforms...");

            var Platforms = _repository.GetAllPlatforms();

            //Mapping to PlatformReadDTO From Platforms(Platform Model)
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(Platforms));
        }

        //Name Here is RouteName used in another endpoints to refer to this endpoint
        [HttpGet("{id}", Name = "GetPlatformByID")]

        public ActionResult<PlatformReadDTO>GetPlatformByID(int id)
        {
            var platformItem = _repository.GetPlatformByID(id);

            if(platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>>CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            //Mapping From CreateDTO TO Platform Model
            var PlatformModel = _mapper.Map<Platform>(platformCreateDTO);

            _repository.CreatPlatform(PlatformModel);
            _repository.SaveChanges();

            var PlatformreadDTO = _mapper.Map<PlatformReadDTO>(PlatformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(PlatformreadDTO);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could Not send synchrounsly: {ex.Message}");
            }

            //routname, routevalues, object value
            return CreatedAtRoute("GetPlatformByID", new { id = PlatformModel.Id }, PlatformreadDTO);
        }
    }
}
