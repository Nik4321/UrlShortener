using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Dtos.Errors;
using UrlShortener.Data.Models.Dtos.Url;
using UrlShortener.Services;

namespace UrlShortener.API.Controllers
{
    /// <summary>
    /// Controller for Urls
    /// </summary>
    [ApiController]
    public class UrlController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUrlService urlService;

        /// <summary>
        /// Constructs an instance of <see cref="UrlController"/>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="urlService"></param>
        public UrlController(IMapper mapper, IUrlService urlService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
        }

        /// <summary>
        /// Redirects to long url using short url
        /// </summary>
        /// <param name="shortUrl">Short url that will be used to find long url</param>
        /// <returns></returns>
        [HttpGet("/{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(string shortUrl)
        {
            var url = await this.urlService.GetUrl(shortUrl);
            if (url == null) return this.GetUrlErrorResponse(400, "Not found", "Url not found");

            var hasUrlExpired = this.urlService.HasUrlExpired(url);
            if (hasUrlExpired)
            {
                return this.GetUrlErrorResponse(400, "Not found", "Url expired");
            }

            return this.Redirect(url.LongUrl);
        }

        /// <summary>
        /// Creates a shorten url
        /// </summary>
        /// <param name="model">CreateUrl model</param>
        /// <returns></returns>
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