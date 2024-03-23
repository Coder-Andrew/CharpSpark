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

public class ApplicationTrackerController : Controller
{
    private readonly ILogger<ApplicationTrackerController> _logger;
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<ApplicationTracker> _applicationTrackerRepository;
    private readonly IApplicationTrackerService _applicationTrackerService;

    public ApplicationTrackerController(
        ILogger<ApplicationTrackerController> logger,
        IRepository<UserInfo> userInfo,
        UserManager<ApplicationUser> userManager,
        IRepository<ApplicationTracker> appTrackerRepo,
        IApplicationTrackerService applicationTrackerService
        )
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
        _applicationTrackerRepository = appTrackerRepo;
        _applicationTrackerService = applicationTrackerService;
    }

    public IActionResult Index()
    {
        string currUserId = _userManager.GetUserId(User)!;
        if (currUserId == null)
        {
            return RedirectToAction("Index", "Home");
        }
        var user = _userInfo.GetAll().Where(x => x.AspnetIdentityId == currUserId).FirstOrDefault();
        if (user == null)
        {
            return View();
        }
        ViewBag.UserId = user.Id;
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}