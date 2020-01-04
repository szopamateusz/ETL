using System;
using System.Net.Mime;
using System.Threading.Tasks;
using ETL.API.Data;
using ETL.API.Models;
using ETL.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETL.API.Controllers
{
    [Route("/etl/api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class EtlController : ControllerBase
    {
        private readonly IHtmlExtractor _htmlExtractor;
        private readonly IHtmlTransformer _htmlTransformer;
        private readonly IHtmlLoader _htmlLoader;
        private readonly ReviewsDbContext _reviewsDbContext;
        public EtlController(IHtmlExtractor htmlExtractor, IHtmlTransformer htmlTransformer, IHtmlLoader htmlLoader, ReviewsDbContext reviewsDbContext)
        {
            _htmlExtractor = htmlExtractor;
            _htmlTransformer = htmlTransformer;
            _htmlLoader = htmlLoader;
            _reviewsDbContext = reviewsDbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _reviewsDbContext.Reviews.ToListAsync();
            
            return Ok(reviews);
        }

        [HttpPost("extract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Extract([FromBody]UrlModel url)
        {
            var extractResult = await _htmlExtractor.Extract(url.Url);

            return Ok(extractResult);
        } 

        [HttpPost("transform")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Transform()
        {
            var transformResult = await _htmlTransformer.Transform();

            return Ok(transformResult);
        }

        [HttpPost("load")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Load()
        {
            var loadResult = await _htmlLoader.Load();
            return Ok(loadResult);
        }

        [HttpPost("etl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ETL([FromBody]UrlModel url)
        {
            var extractResult = await _htmlExtractor.Extract(url.Url);
            var transformResult = await _htmlTransformer.Transform();
            var loadResult = await _htmlLoader.Load();

            string processResult = $"{extractResult}  {transformResult}  {loadResult}";
            return Ok(processResult);
        }

        [HttpDelete("clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Clear()
        {
            await _reviewsDbContext.Database.ExecuteSqlRawAsync(@"TRUNCATE TABLE Reviews");

            return Ok();
        }
    }
}