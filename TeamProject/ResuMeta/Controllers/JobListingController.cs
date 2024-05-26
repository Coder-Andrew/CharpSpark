using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Collections.Generic;
using System.Linq;
using ResuMeta.Services.Abstract;
using ResuMeta.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ResuMeta.Controllers;

public class JobListingController : Controller
{
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IResumeRepository _resumeRepository;
    private readonly IResumeService _resumeService;
    public JobListingController(IRepository<UserInfo> userInfo, UserManager<ApplicationUser> userManager, IResumeRepository resumeRepository, IResumeService resumeService)
    {
        _userInfo = userInfo;
        _userManager = userManager;
        _resumeRepository = resumeRepository;
        _resumeService = resumeService;
    }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            List<ResumeVM> resumes = new List<ResumeVM>();
            if (user != null)
            {
                // User is logged in, you can access user.Id
                var userId = user.Id;
                UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == userId).FirstOrDefault()!;
                resumes = _resumeRepository.GetAllResumes(currUser.Id);
            }
            ViewBag.Resumes = resumes.Select(r => new SelectListItem { Value = r.ResumeId.ToString(), Text = r.Title }).ToList();

            JobListingVM jobListing = new JobListingVM();
            return View("Index", jobListing);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(JobListingVM jobListing)
    {
        return RedirectToAction("Index", "ApplicationTracker", jobListing);
    }
}
