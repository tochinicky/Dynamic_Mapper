using Microsoft.AspNetCore.Mvc;
using DIRS21.Mapper.Application.Mapping;
using DIRS21.Mapper.Domain.Exceptions;

namespace DIRS21.Mapper.Presentation.Controllers.MapperHandler
{
    [ApiController]
    public class MappingController : ControllerBase
    {
        private readonly MapHandler _mapHandler;
         public MappingController(MapHandler mapHandler)
        {
            _mapHandler = mapHandler ?? throw new ArgumentNullException(nameof(mapHandler));
        }

        [HttpPost("map")]
        public IActionResult MapData([FromBody] MappingRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.SourceType) || string.IsNullOrEmpty(request.TargetType) || request.Data == null)
            {
                return BadRequest("Invalid mapping request.");
            }

            try
            {
                var result = _mapHandler.Map(request.Data, request.SourceType, request.TargetType);
                return Ok(result);
            }
            catch (MappingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidMappingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
