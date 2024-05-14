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
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ResuMeta.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IRepository<UserInfo> _userInfo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Resume> _resumeRepository;
    private readonly IResumeService _resumeService;
    private readonly IRepository<Profile> _profileRepository;
    private readonly IProfileService _profileService;
    public ProfileController(
        ILogger<ProfileController> logger,
        IRepository<UserInfo> userInfo,
        UserManager<ApplicationUser> userManager,
        IRepository<Resume> resumeRepo,
        IResumeService resumeService,
        IRepository<Profile> profileRepository,
        IProfileService profileService
        )
    {
        _logger = logger;
        _userInfo = userInfo;
        _userManager = userManager;
        _resumeRepository = resumeRepo;
        _resumeService = resumeService;
        _profileRepository = profileRepository;
        _profileService = profileService;
    }

    public IActionResult Index()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        Profile? userProfile = _profileRepository.GetAll().Where(x => x.UserInfoId == currUser.Id).FirstOrDefault();
        if (userProfile == null)
        {
            ViewBag.User = currUser;
            ViewBag.UserName = _userManager.GetUserName(User);
            List<ResumeVM> resumeList = _resumeService.GetAllResumes(currUser.Id);
            ViewBag.Resumes = resumeList;
            return View();
        }
        else
        {
            return RedirectToAction("YourProfile", "Profile");
        }
    }

    [HttpPost]
    public IActionResult Index(ProfileVM profile)
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        if (profile.Description == null)
        {
            ViewBag.User = currUser;
            ViewBag.UserName = _userManager.GetUserName(User);
            List<ResumeVM> resumeList = _resumeService.GetAllResumes(currUser.Id);
            ViewBag.Resumes = resumeList;
            return View(profile);
        }
        Profile? userProfile = _profileRepository.GetAll().Where(x => x.UserInfoId == currUser.Id).FirstOrDefault();
        if (userProfile != null)
        {
            return RedirectToAction("YourProfile", "Profile");
        }
        try
        {
            Profile newProfile = new Profile
            {
                UserInfoId = currUser.Id,
                Resume = profile.Resume,
                Description = profile.Description
            };
            _profileRepository.AddOrUpdate(newProfile);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error saving profile");
            return RedirectToAction("Index", "Profile");
        }
        return RedirectToAction("YourProfile", "Profile");
    }

    public async Task<IActionResult> EditProfile()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        Profile? userProfile = _profileRepository.GetAll().Where(x => x.UserInfoId == currUser.Id).FirstOrDefault();
        if (userProfile != null)
        {
            ProfileVM profileVM = await _profileService.GetProfile(userProfile.Id);
            List<ResumeVM> resumeList = _resumeService.GetAllResumes(currUser.Id);
            ViewBag.Resumes = resumeList;
            return View(profileVM);
        }
        else
        {
            return RedirectToAction("Index", "Profile");
        }
    }

    [HttpPost]
    public IActionResult EditProfile(ProfileVM profile)
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        if (profile.Description == null)
        {
            List<ResumeVM> resumeList = _resumeService.GetAllResumes(currUser.Id);
            ViewBag.Resumes = resumeList;
            return View(profile);
        }
        try
        {
            bool result = _profileService.SaveProfile(currUser.Id, profile);
            if (!result)
            {
                return RedirectToAction("EditProfile", "Profile");
            }
            return RedirectToAction("YourProfile", "Profile");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error saving profile");
            return RedirectToAction("Index", "Profile");
        }
    }

    public async Task<IActionResult> YourProfile()
    {
        string id = _userManager.GetUserId(User)!;
        if (id == null)
        {
            return RedirectToAction("Index", "Home");
        }
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        Profile? userProfile = _profileRepository.GetAll().Where(x => x.UserInfoId == currUser.Id).FirstOrDefault();
        if (userProfile == null)
        {
            return RedirectToAction("Index", "Profile");
        }
        ProfileVM profileVM = await _profileService.GetProfile(userProfile.Id);
        return View(profileVM);
    }

    [AllowAnonymous]
    public async Task<IActionResult> UserProfile(int id)
    {
        try 
        {
            ProfileVM profileVM = await _profileService.GetProfile(id);
            return View(profileVM);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting profile");
            return RedirectToAction("Index", "Home");
        }
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}