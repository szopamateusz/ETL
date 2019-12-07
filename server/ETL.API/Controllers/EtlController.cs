using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETL.API.Controllers
{
    [Route("/etl/api")]
    [ApiController]
    public class EtlController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReviews()
        {
            return Ok();
        }

        [HttpPost("extract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Extract([FromBody] string url)
        {
            return Ok();
        }

        [HttpPost("transform")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Transform()
        {
            return Ok();
        }

        [HttpPost("load")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Load()
        {
            return Ok();
        }

        [HttpPost("etl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ETL([FromBody] string url)
        {
            return Ok();
        }

        [HttpDelete("clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Clear()
        {
            return Ok();
        }
    }
}