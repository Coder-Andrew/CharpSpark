// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;

namespace ResuMeta.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ResuMetaDbContext _ResuMetaDbContext;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ResuMetaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ResuMetaDbContext = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Summary { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Display(Name = "New First Name")]
            public string NewFirstName { get; set; }

            [Display(Name = "New Last Name")]
            public string NewLastName { get; set; }

            [Display(Name = "New Summary")]
            public string NewSummary { get; set; }

            [Phone]
            [Display(Name = "New Phone Number")]
            public string NewPhoneNumber { get; set; }

            [Display(Name = "New Username")]
            public string NewUsername { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var currentUser = await _ResuMetaDbContext.UserInfos.FirstOrDefaultAsync(u => u.AspnetIdentityId == userId);

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = currentUser.FirstName;
            var lastName = currentUser.LastName;
            var summary = currentUser.Summary;

            Username = userName;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Summary = summary;

            Input = new InputModel
            {
                NewFirstName = "",
                NewLastName = "",
                NewSummary = "",
                NewPhoneNumber = "",
                NewUsername = ""
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeProfileAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var currentUser = await _ResuMetaDbContext.UserInfos.FirstOrDefaultAsync(u => u.AspnetIdentityId == userId);
            if (user == null || currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            if (Input.NewUsername != userName && !string.IsNullOrEmpty(Input.NewUsername))
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewUsername);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set user name.";
                    return RedirectToPage();
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.NewPhoneNumber != phoneNumber && !string.IsNullOrEmpty(Input.NewPhoneNumber))
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.NewPhoneNumber);
                currentUser.PhoneNumber = Input.NewPhoneNumber;
                var changes = await _ResuMetaDbContext.SaveChangesAsync();

                if (!setPhoneResult.Succeeded || changes < 0)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //Non-ASP.NET Core Identity fields
            var firstName = currentUser.FirstName;
            var lastName = LastName;
            var summary = Summary;
            
            if (Input.NewFirstName != firstName && !string.IsNullOrEmpty(Input.NewFirstName))
            {
                currentUser.FirstName = Input.NewFirstName;
                var changes = await _ResuMetaDbContext.SaveChangesAsync();
                if (changes < 0)
                {
                    StatusMessage = "Unexpected error when trying to set first name.";
                    return RedirectToPage();
                }
            }

            if (Input.NewLastName != lastName && !string.IsNullOrEmpty(Input.NewLastName))
            {
                currentUser.LastName = Input.NewLastName;
                var changes = await _ResuMetaDbContext.SaveChangesAsync();
                if (changes < 0)
                {
                    StatusMessage = "Unexpected error when trying to set last name.";
                    return RedirectToPage();
                }
            }

            if (Input.NewSummary != summary && !string.IsNullOrEmpty(Input.NewSummary))
            {
                currentUser.Summary = Input.NewSummary;
                var changes = await _ResuMetaDbContext.SaveChangesAsync();
                if (changes < 0)
                {
                    StatusMessage = "Unexpected error when trying to set summary.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
