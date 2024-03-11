using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using ResuMeta.ViewModels;
using System.Linq;
using ResuMeta.Services.Concrete;
using ResuMeta.Services.Abstract;
using ResuMeta.Data;

namespace ResuMeta.Controllers;

public class CoverLetterController : Controller
{
    private readonly ILogger<ResumeController> _logger;
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;
    public CoverLetterController(
        ILogger<ResumeController> logger,
        IRepository<UserInfo> userInfo,
        UserManager<ApplicationUser> userManager
        )
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateCoverLetter()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult ViewCoverLetter()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}