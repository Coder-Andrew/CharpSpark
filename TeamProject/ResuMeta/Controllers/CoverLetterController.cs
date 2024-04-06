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
    private readonly ICoverLetterService _coverLetterService;
    public CoverLetterController(
        ILogger<ResumeController> logger,
        IRepository<UserInfo> userInfo,
        UserManager<ApplicationUser> userManager,
        ICoverLetterService coverLetterService
        )
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
        _coverLetterService = coverLetterService;

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
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        UserVM userVM = new UserVM(); 
        userVM.UserId = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!.Id;
        return View(userVM);
    }

    [HttpGet("CoverLetter/ViewCoverLetter/{coverLetterId}")]
    public IActionResult ViewCoverLetter(int coverLetterId)
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }

        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        CoverLetterVM coverLetterVM =  _coverLetterService.GetCoverLetter(coverLetterId);

        if(coverLetterVM.UserInfoId != currUser.Id)
        {
            return RedirectToAction("Index", "Home");
        }
        
        return View(coverLetterVM);
    }
}