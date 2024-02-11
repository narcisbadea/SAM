using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAM.Application;
using SAM.Domain;

namespace SAM.Api.Controllers;

[Route("api/")]
[ApiController]
public class TopSellingAppsController : ControllerBase
{
    private readonly IWebScraping _webSCraping;

    public TopSellingAppsController(IWebScraping webSCraping)
    {
        _webSCraping = webSCraping;
    }

    [HttpPost("get-app")]
    public async Task<ActionResult<ApplicationModel>> Test([FromQuery]string appname)
    {
        return Ok(_webSCraping.CallUrl(appname));
    }

    [HttpGet("top-selling/free")]
    public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetFree([FromQuery] int number)
    {
        return Ok(await _webSCraping.CallTOP(number));
    }

    [HttpGet("top-selling/paid")]
    public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetPaid([FromQuery]int number)
    {
        return Ok(await _webSCraping.CallTopPaid(number));
    }
}
