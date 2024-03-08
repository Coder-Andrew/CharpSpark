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
    // private readonly IRepository<Resume> _resumeRepository;
    // private readonly IResumeService _resumeService;

    public ApplicationTrackerController(
        ILogger<ApplicationTrackerController> logger,
        IRepository<UserInfo> userInfo,
        UserManager<ApplicationUser> userManager
        // IRepository<Resume> resumeRepo,
        // IResumeService resumeService
        )
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
        // _resumeRepository = resumeRepo;
        // _resumeService = resumeService;
    }

    public IActionResult Index()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}