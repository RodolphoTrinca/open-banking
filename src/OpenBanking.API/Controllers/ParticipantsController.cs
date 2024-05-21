using Microsoft.AspNetCore.Mvc;
using OpenBanking.API.DTO;
using OpenBanking.Application.Interfaces;

namespace OpenBanking.API.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IBankDataService _service;
        private readonly ILogger<ParticipantsController> _logger;

        public ParticipantsController(IBankDataService participantsService, ILogger<ParticipantsController> logger)
        {
            _service = participantsService;
            _logger = logger;
        }

        //GET api/participants
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int skip = 0, [FromQuery] int take = 100)
        {
            try
            {
                var listBanks = _service.GetAll(skip, take);

                if (listBanks is null)
                {
                    return NotFound();
                }

                var dto = new ParticipantsDTO(listBanks);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to get participating banks");
                return BadRequest();
            }
        }
    }
}