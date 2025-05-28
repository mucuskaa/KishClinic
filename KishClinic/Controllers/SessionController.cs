using KishClinic.Entities;
using KishClinic.Models;
using KishClinic.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace KishClinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetAll()
        {
            var sessions = await _sessionService.GetAllAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetById(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetByUser(int userId)
        {
            var sessions = await _sessionService.GetAllAsync();
            var userSessions = sessions.Where(s => s.ClientID == userId);
            return Ok(userSessions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = new Session
            {
                ClientID = dto.ClientID,
                SessionDate = dto.SessionDate,
                StartTime = dto.StartTime,
                FinishTime = dto.FinishTime,
                Status = dto.Status,
                Notes = dto.Notes,
                SessionTypeID = dto.SessionTypeID
            };

            var createdSession = await _sessionService.CreateAsync(session);
            return Ok(createdSession);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Session>> Update(int id, [FromBody] SessionDto sessionDto)
        {
            var session = new Session
            {
                ClientID = sessionDto.ClientID,
                SessionDate = sessionDto.SessionDate,
                StartTime = sessionDto.StartTime,
                FinishTime = sessionDto.FinishTime,
                Status = sessionDto.Status,
                Notes = sessionDto.Notes,
                SessionTypeID = sessionDto.SessionTypeID
            };


            var updatedSession = await _sessionService.UpdateAsync(id, session);
            if (updatedSession == null)
            {
                return NotFound();
            }
            return Ok(updatedSession);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Session>> UpdateStatus(int id, [FromBody] SessionStatusUpdateDto statusDto)
        {
            if (statusDto == null || string.IsNullOrEmpty(statusDto.Status))
            {
                return BadRequest("Status is required");
            }

            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            session.Status = statusDto.Status;
            var updatedSession = await _sessionService.UpdateAsync(id, session);
            if (updatedSession == null)
            {
                return NotFound();
            }

            return Ok(updatedSession);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Session>> Update(int id, [FromBody] JsonPatchDocument<Session> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(session, (error) => ModelState.AddModelError(
                error.AffectedObject?.ToString() ?? "Unknown",
                error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSession = await _sessionService.UpdateAsync(id, session);
            if (updatedSession == null)
            {
                return NotFound();
            }

            return Ok(updatedSession);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _sessionService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
