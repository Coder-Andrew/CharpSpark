// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ResuMeta.Data;

namespace ResuMeta.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SecurityQuestionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ILogger<SecurityQuestionsModel> _logger;

        public SecurityQuestionsModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IPasswordHasher<ApplicationUser> passwordHasher, ILogger<SecurityQuestionsModel> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            public string Answer1 { get; set; }

            [Required]
            public string Answer2 { get; set; }

            [Required]
            public string Answer3 { get; set; }
        }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await GetUserAsync();

        if (user == null)
        {
            return NotFound("User not found");
        }

        SecurityQuestion1 = user.SecurityQuestion1;
        SecurityQuestion2 = user.SecurityQuestion2;
        SecurityQuestion3 = user.SecurityQuestion3;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(InputModel input)
    {
        var user = await GetUserAsync();

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "No user found with the provided email.");
            return Page();
        }

        var answersFromUser = new List<string>
        {
            input.Answer1,
            input.Answer2,
            input.Answer3
        };

        var securityQuestions = GetSecurityQuestionsForUser(user);

        if (AreAnswersCorrect(securityQuestions, answersFromUser))
        {
            _logger.LogInformation("Security questions answered correctly. Redirecting to Reset Password page.");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);
            return RedirectToPage("/Account/ResetPassword", new { area = "Identity", code = code });
        }
        else
        {
            _logger.LogInformation("Security questions answered incorrectly. ");

            ModelState.AddModelError(string.Empty, "The answers to the security questions are not correct.");
            return Page();
        }
    }

    private async Task<ApplicationUser> GetUserAsync()
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogError("No user id found in session");
            return null;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            _logger.LogError("User not found with id");
        }

        return user;
    }

        private List<string> GetSecurityQuestionsForUser(ApplicationUser user)
        {
            var securityQuestionAnswers = new List<string>
            {
                user.SecurityAnswer1,
                user.SecurityAnswer2,
                user.SecurityAnswer3
            };

            return securityQuestionAnswers;
        }


        private bool AreAnswersCorrect(List<string> hashedAnswersFromDb, List<string> answersFromUser)
        {
            for (int i = 0; i < hashedAnswersFromDb.Count; i++)
            {
                var result = _passwordHasher.VerifyHashedPassword(null, hashedAnswersFromDb[i], answersFromUser[i]);

                if (result == PasswordVerificationResult.Failed)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
