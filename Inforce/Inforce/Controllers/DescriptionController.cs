using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.GetShortUrls;
using Inforce.Model;
using Inforce.Service.DescriptionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.Controllers;

[Route("api")]
[ApiController]
public class DescriptionController : Controller
{
    private readonly IDescriptionRepository _descriptionRepository;
    private readonly IDescriptionService _descriptionService;
    public DescriptionController(IDescriptionRepository descriptionRepository,
        IDescriptionService descriptionService)
    {
        _descriptionRepository = descriptionRepository;
        _descriptionService = descriptionService;
    }

    [HttpGet("short-urls/{shortUrlId:int}/description"), AllowAnonymous]
    public async Task<IActionResult> GetDescription([FromRoute] int shortUrlId,
        CancellationToken cancellationToken=default)
    {
        var a = (await _descriptionRepository.GetAll(cancellationToken))
            .Select(desription => new { Description = desription!.Text }).FirstOrDefault();

        return Ok(a);
    }
    [HttpPut("short-urls/{shortUrlId:int}/description"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDescription([FromRoute] int shortUrlId,
        UpdateDescription text, CancellationToken cancellationToken=default)
    {
        await _descriptionService.UpdateDescription(shortUrlId, text, cancellationToken);

        return Ok();
    }
}