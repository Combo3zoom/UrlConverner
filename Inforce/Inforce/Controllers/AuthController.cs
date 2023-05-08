using System.Security.Claims;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.Model;
using Inforce.Service.Registration;
using Inforce.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Inforce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRegisterUserService _registerUserService;
    private readonly IUserService _userService;

    public AuthController(IRegisterUserService registerUserService, UserManager<User> userManager,
        SignInManager<User> signInManager, IUserService userService)
    {
        _registerUserService = registerUserService;
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
    }

    [HttpGet, Authorize]
    public ActionResult<string> GetMyId()
    {
        _registerUserService.GetId();
        return Ok();
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken=default)
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RequestRegisterUser request, CancellationToken cancellationToken=default)
    {
        var newUser = new User{Id=new Guid(), Name=request.Name!, UserName = request.Name};
        var result = await _userManager.CreateAsync(newUser, request.Password);
        await _userManager.AddToRoleAsync(newUser, "User");
        
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _signInManager.SignInAsync(newUser, isPersistent: false);

        return CreatedAtAction(nameof(GetMyId),new{id=newUser.Id}, newUser);
    } 

    [HttpPost("login")]
    public async Task<ActionResult<RoleResponse>> Login(RequestRegisterUser request, CancellationToken cancellationToken=default)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user =  await _userService.GetByName(request.Name!, cancellationToken);

        var checkPassword = await _signInManager.PasswordSignInAsync(request.Name, request.Password, 
            false, false);
        if (!checkPassword.Succeeded)
            return BadRequest("Incorrect name or password");

        await _signInManager.CanSignInAsync(user);
        await _registerUserService.Save(cancellationToken);

        var roles = await _userManager.GetRolesAsync(user);
        var userRole = new RoleResponse(roles.FirstOrDefault()!);
        return Ok(userRole);
    }

} 