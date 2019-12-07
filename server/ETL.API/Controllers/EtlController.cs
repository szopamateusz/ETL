using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETL.API.Models;
using ETL.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETL.API.Controllers
{
    [Route("/etl/api")]
    [ApiController]
    public class EtlController : ControllerBase
    {
        private readonly IHtmlExtractor _htmlExtractor;
        private readonly IHtmlTransformer _htmlTransformer;

        public EtlController(IHtmlExtractor htmlExtractor, IHtmlTransformer htmlTransformer)
        {
            _htmlExtractor = htmlExtractor;
            _htmlTransformer = htmlTransformer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = new List<Review>
            {
                new Review
                {
                    ReviewerName = "Mateusz",
                    ReviewTitle = "Bad product",
                    ProductRating = "2 of 5 stars",
                    ReviewDate = "28 Maja 2019",
                    ReviewText = "abcdefghijklmnopqrstuwvxyz"
                }
            };

            return Ok(reviews);
        }

        [HttpPost("extract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Extract([FromBody] string url)
        {
            await _htmlExtractor.Extract(url);

            return Ok();
        }

        [HttpPost("transform")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Transform()
        {
            _htmlTransformer.Transform(Environment.CurrentDirectory + @"\webpage.txt");

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