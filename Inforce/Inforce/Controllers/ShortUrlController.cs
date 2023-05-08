using System.Security.Claims;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.GetShortUrls;
using Inforce.Model;
using Inforce.Service.UrlService;
using Inforce.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.Controllers;

[Route("api")]
[ApiController]
public class ShortUrlController: Controller
{
    private readonly IUserService _userService;
    private readonly IShortedUrlService _shortedUrlService;
    
    public ShortUrlController(IShortedUrlService shortedUrlService, IUserService userService)
    {
        _shortedUrlService = shortedUrlService;
        _userService = userService;
    }

    [HttpGet("short-urls"), AllowAnonymous]
    public async Task<ShortUrlResponse[]> GetUrls(CancellationToken cancellationToken=default)
    {
        return await _shortedUrlService.GetUrls(cancellationToken);
    }
    
    [HttpGet("me/short-urls"), Authorize(Roles = "Admin, User")]
    public async Task<ShortUrlResponse[]> GetMyUrls(CancellationToken cancellationToken=default)
    {
        var userId = _userService.GetId();
        var user = await _userService.GetById(userId, cancellationToken);
        
        return await _shortedUrlService.GetMyUrls(user!, cancellationToken);
    }

    [HttpPost("short-urls"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> CreateShortUrl([FromBody] CreateUrlBody createUrlBody,
        CancellationToken cancellationToken = default)
    {
        var userId = _userService.GetId();
        return Ok(await _shortedUrlService.CreateShortUrl(createUrlBody, userId, cancellationToken));
    }
    
    [HttpDelete("short-urls/{urlId:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAnyShortUrl([FromRoute] int urlId,
        CancellationToken cancellationToken = default)
    {
        var url = await _shortedUrlService.GetById(urlId, cancellationToken);
        await _shortedUrlService.DeleteAnyShortUrl(url, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("short-urls/{urlId:int}/redirect"), AllowAnonymous]
    public async Task<IActionResult> RedirectToSourceUrl([FromRoute] int urlId,
        CancellationToken cancellationToken = default)
    {
        var url = await _shortedUrlService.GetById(urlId, cancellationToken);

        return Redirect(url.SourceUrl);
    }
    
    [HttpDelete("me/short-urls/{urlId:int}"), Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteOwnedShortUrl([FromRoute] int urlId,
        CancellationToken cancellationToken = default)
    {
        var issuerUserId = _userService.GetId();
        
        var url = await _shortedUrlService.GetById(urlId, cancellationToken);
        await _shortedUrlService.DeleteOwnedShortUrl(url, issuerUserId, cancellationToken);

        return Ok();
    }
}