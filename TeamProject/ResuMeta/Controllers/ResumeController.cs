using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using ResuMeta.ViewModels;
using System.Linq;

namespace ResuMeta.Controllers;

public class ResumeController : Controller
{
    private readonly ILogger<ResumeController> _logger;
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<IdentityUser> _userManager;

    public ResumeController(ILogger<ResumeController> logger, IRepository<UserInfo> userInfo, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateResume()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserVM userVM = new UserVM(); 
        userVM.UserId = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!.Id;
        return View(userVM);
    }

    public IActionResult ViewResume()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
