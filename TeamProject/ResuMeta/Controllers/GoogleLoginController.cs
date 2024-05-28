using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.DAL.Abstract;
using ResuMeta.Data;
using ResuMeta.Models;
using System.Security.Claims;

namespace ResuMeta.Controllers
{
    [AllowAnonymous]
    public class GoogleLoginController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IRepository<UserInfo> _userInfo;
        public GoogleLoginController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRepository<UserInfo> userInfo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userInfo = userInfo;
        }


        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties()
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // Authentication failed, redirect to home.
            }

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var googleIdClaim = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (googleIdClaim == null)
            {
                return RedirectToAction("Index", "Home"); // No valid Google ID claim, redirect to home.
            }

            var user = await _userManager.FindByLoginAsync("Google", googleIdClaim.Value);

            if (user == null)
            {
                // User not found, create a new user.
                var emailClaim = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                user = new ApplicationUser
                {
                    UserName = emailClaim?.Value ?? googleIdClaim.Value,
                    Email = emailClaim?.Value
                };
                var identityResult = await _userManager.CreateAsync(user);

                if (!identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // User creation failed, redirect to home.
                }

                identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", googleIdClaim.Value, "Google"));
                if (!identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Adding Google login failed, redirect to home.
                }

                var appUser = new UserInfo
                {
                    FirstName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
                    LastName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
                    PhoneNumber = claims?.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value ?? claims?.FirstOrDefault(c => c.Type == ClaimTypes.HomePhone)?.Value ?? claims?.FirstOrDefault(c => c.Type == ClaimTypes.OtherPhone)?.Value,
                    Email = emailClaim?.Value,
                    AspnetIdentityId = user.Id
                };

                _userInfo.AddOrUpdate(appUser);
        }

            // Sign in the user.
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

    }
}
