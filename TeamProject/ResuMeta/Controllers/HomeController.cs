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
namespace ResuMeta.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IRepository<Resume> _resumeRepository;
    private readonly IResumeService _resumeService;
    public HomeController(ILogger<HomeController> logger, IRepository<UserInfo> userInfo, UserManager<IdentityUser> userManager, IRepository<Resume> resumeRepo, IResumeService resumeService)
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
        _resumeRepository = resumeRepo;
        _resumeService = resumeService;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        string currUserId = _userManager.GetUserId(User);
        if (currUserId == null)
        {
            return View();
        }    
        var user = _userInfo.GetAll().Where(x => x.AspnetIdentityId == currUserId).FirstOrDefault();
        if (user == null)
        {
            return View();
        }
        List<KeyValuePair<int, string>> resumeIdList = _resumeService.GetResumeIdList(user.Id);
        return resumeIdList.Any() ? View(resumeIdList) : View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [AllowAnonymous]
    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
