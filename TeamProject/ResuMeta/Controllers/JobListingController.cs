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
namespace ResuMeta.Controllers;

public class JobListingController : Controller
{
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;
    public JobListingController(IRepository<UserInfo> userInfo, UserManager<ApplicationUser> userManager)
    {
        _userInfo = userInfo;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        JobListingVM jobListing = new JobListingVM();
        return View("Index", jobListing);
    }

    [HttpPost]
    public IActionResult Index(JobListingVM jobListing)
    {
        return RedirectToAction("Index", "ApplicationTracker", jobListing);
    }
}
