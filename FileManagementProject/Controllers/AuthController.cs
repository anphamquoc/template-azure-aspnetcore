using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace FileManagementProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    // For getting the user info
    [HttpGet("me")]
    public IActionResult Me()
    {
        if (User.Identity is { IsAuthenticated: true })
            return Ok(new
            {
                email = User.Identity.Name,
                name = User.FindFirst("name")?.Value
            });

        return Unauthorized();
    }

    [HttpGet("signin")]
    public IActionResult SignIn()
    {
        var redirectUrl = Url.Action(nameof(HandleSignIn), "Auth", null, Request.Scheme);

        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        };

        return Challenge(properties, "OpenIdConnect");
    }

    [HttpGet("signout")]
    public IActionResult SignOut()
    {
        var redirectUrl = Url.Action(nameof(HandleSignout), "Auth", null, Request.Scheme);
        return SignOut(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("handlesignin")]
    public async Task<IActionResult> HandleSignIn()
    {
        var result = await HttpContext.AuthenticateAsync();
        if (result.Succeeded) return Redirect("https://localhost:5500");

        return BadRequest("Login failed");
    }

    [HttpGet("handlesignout")]
    public async Task<IActionResult> HandleSignout()

    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        return Redirect("/api/Auth/signin");
    }
}