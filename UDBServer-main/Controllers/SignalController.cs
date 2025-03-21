using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UDPForward.Models;
using UDPForward.Services;

namespace UDPForward.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ConnectionService _connectionService;

        public SignalController(ConnectionService connectionService) 
        {
            _connectionService = connectionService;
        }

        // Tạo Offer và trả về ConnectionId
        [HttpPost("offer")]
        public IActionResult CreateOffer([FromBody] Offer offer)
        {
            var connectionId = _connectionService.CreateOffer(offer);
            return Ok(new { ConnectionId = connectionId });
        }

        // Nhận Answer từ Client
        [HttpPost("answer/{connectionId}")]
        public IActionResult AddAnswer(string connectionId, [FromBody] Offer answer)
        {
            var success = _connectionService.AddAnswer(connectionId, answer);
            if (!success) return NotFound("Connection not found");
            return Ok();
        }

        // Nhận ICE Candidate từ Client
        [HttpPost("candidate/{connectionId}")]
        public IActionResult AddCandidate(string connectionId, [FromBody] IceCandidate candidate)
        {
            var success = _connectionService.AddCandidate(connectionId, candidate);
            if (!success) return NotFound("Connection not found");
            return Ok();
        }

        // Lấy Offer dựa trên ConnectionId
        [HttpGet("offer/{connectionId}")]
        public IActionResult GetOffer(string connectionId)
        {
            var offer = _connectionService.GetOffer(connectionId);
            if (offer == null) return NotFound("Offer not found");
            return Ok(offer);
        }

        // Lấy Answer dựa trên ConnectionId
        [HttpGet("answer/{connectionId}")]
        public IActionResult GetAnswer(string connectionId)
        {
            var answer = _connectionService.GetAnswer(connectionId);
            if (answer == null) return NotFound("Answer not found");
            return Ok(answer);
        }

        // Lấy ICE Candidate
        [HttpGet("candidate/{connectionId}")]
        public IActionResult GetCandidates(string connectionId)
        {
            var candidates = _connectionService.GetCandidates(connectionId);
            if (candidates == null || !candidates.Any()) return NotFound("No candidates found");
            return Ok(candidates);
        }
    }
}
