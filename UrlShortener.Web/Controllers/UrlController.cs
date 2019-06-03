using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.Errors;
using UrlShortener.Models.Url;
using UrlShortener.Services;

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
        public IActionResult Get(string shortUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var url = this.urlService.GetUrlByShortUrl(shortUrl);
            if (url == null) return this.GetUrlErrorResponse(400, "Not found", "Url not found");

            var hasUrlExpired = this.urlService.HasUrlExpired(url);
            if (hasUrlExpired)
            {
                return this.GetUrlErrorResponse(400, "Not found", "Url expired");
            }

            return this.Redirect(url.LongUrl);
        }

        [HttpPost("/")]
        public IActionResult ShortenUrl([FromQuery] CreateUrl model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.urlService.ShortenUrl(model.LongUrl, model.ExpireDate);
            var response = this.mapper.Map<ResponseUrl>(result);
            response.ShortUrl = this.FormatShortUrlResponse(response.ShortUrl);
            return this.Created(nameof(ShortenUrl), response);
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