using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs.Request;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var result = await _userService.UserLogin(request);

        if (result != null)
        {
            return Ok(result);
        }
        
        return BadRequest("Invalid login or password");
    }
}