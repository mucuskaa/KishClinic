using KishClinic.Entities;
using KishClinic.Models;
using KishClinic.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace KishClinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionTypeController : ControllerBase
    {
        private readonly ISessionTypeService _sessionTypeService;

        public SessionTypeController(ISessionTypeService sessionTypeService)
        {
            _sessionTypeService = sessionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionType>>> GetAll()
        {
            var sessionTypes = await _sessionTypeService.GetAllAsync();
            return Ok(sessionTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionType>> GetById(int id)
        {
            var sessionType = await _sessionTypeService.GetByIdAsync(id);
            if (sessionType == null)
            {
                return NotFound();
            }
            return Ok(sessionType);
        }

        [HttpPost]
        public async Task<ActionResult<SessionType>> Create([FromBody] SessionTypeDto sessionTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionType = new SessionType
            {
                Description = sessionTypeDto.Description,
                DefaultFee = sessionTypeDto.DefaultFee,
                DefaultDuration = sessionTypeDto.DefaultDuration
            };

            var createdSessionType = await _sessionTypeService.CreateAsync(sessionType);
            return CreatedAtAction(nameof(GetById), new { id = createdSessionType.ID }, createdSessionType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SessionType>> Update(int id, [FromBody] SessionTypeDto sessionTypeDto)
        {
            var sessionType = new SessionType
            {
                Description = sessionTypeDto.Description,
                DefaultFee = sessionTypeDto.DefaultFee,
                DefaultDuration = sessionTypeDto.DefaultDuration
            };

            var updatedSessionType = await _sessionTypeService.UpdateAsync(id, sessionType);
            if (updatedSessionType == null)
            {
                return NotFound();
            }
            return Ok(updatedSessionType);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SessionType>> Update(int id, [FromBody] JsonPatchDocument<SessionType> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var sessionType = await _sessionTypeService.GetByIdAsync(id);
            if (sessionType == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(sessionType, (error) => ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSessionType = await _sessionTypeService.UpdateAsync(id, sessionType);
            if (updatedSessionType == null)
            {
                return NotFound();
            }

            return Ok(updatedSessionType);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _sessionTypeService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
