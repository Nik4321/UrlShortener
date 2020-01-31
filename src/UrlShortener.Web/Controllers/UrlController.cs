using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.Errors;
using UrlShortener.Models.Url;
using UrlShortener.Services;
using Microsoft.AspNetCore.Http;

namespace UrlShortener.Web.Controllers
{
    [ApiController]
    public class UrlController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUrlService urlService;

        public UrlController(IMapper mapper, IUrlService urlService)
        {
            this.mapper = mapper;
            this.urlService = urlService;
        }

        [HttpGet("/{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(string shortUrl)
        {
            var url = await this.urlService.GetUrlByShortUrl(shortUrl);
            if (url == null) return this.GetUrlErrorResponse(400, "Not found", "Url not found");

            var hasUrlExpired = this.urlService.HasUrlExpired(url);
            if (hasUrlExpired)
            {
                return this.GetUrlErrorResponse(400, "Not found", "Url expired");
            }

            return this.Redirect(url.LongUrl);
        }

        [HttpPost("/")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ShortenUrl([FromBody] CreateUrl model)
        {
            var result = await this.urlService.ShortenUrl(model.LongUrl, model.ExpireDate);
            var response = this.mapper.Map<ResponseUrl>(result);
            response.ShortUrl = this.FormatShortUrlResponse(response.ShortUrl);

            return this.Ok(response);
        }

        private string FormatShortUrlResponse(string shortUrl) =>
            $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{shortUrl}";

        private BadRequestObjectResult GetUrlErrorResponse(int statusCode, string statusDescription, string message)
        {
            var badRequestResponse = new BaseResponseError(statusCode, statusDescription, message);
            return this.BadRequest(badRequestResponse);
        }
    }
}