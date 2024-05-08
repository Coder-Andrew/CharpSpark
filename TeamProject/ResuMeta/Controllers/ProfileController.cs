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
        if (!ModelState.IsValid)
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
        try{
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
            throw new Exception("Error saving profile");
        }
        return RedirectToAction("YourProfile", "Profile");
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
    // public IActionResult YourDashboard()
    // {
    //     string currUserId = _userManager.GetUserId(User)!;
    //     if (currUserId == null)
    //     {
    //         return View();
    //     }    
    //     var user = _userInfo.GetAll().Where(x => x.AspnetIdentityId == currUserId).FirstOrDefault();
    //     if (user == null)
    //     {
    //         return View();
    //     }
    //     List<ResumeVM> resumeList = _resumeService.GetAllResumes(user.Id);
    //     DashboardVM dashboardVM = new DashboardVM
    //     {
    //         Resumes = resumeList,
    //     };
    //     return View(dashboardVM) ?? View();
    // }

    // [HttpGet("Resume/ViewResume/{resumeId}")]
    // public IActionResult ViewResume(int resumeId)
    // {
    //     string id = _userManager.GetUserId(User)!;
    //     if (id == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
    //     Resume userResume = _resumeRepository.GetAll().Where(x => x.Id == resumeId).FirstOrDefault()!;
    //     if (userResume == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     if (userResume.UserInfoId != currUser.Id)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     ApplicationUser idUser = _userManager.FindByIdAsync(id).Result!;
    //     string email = _userManager.GetEmailAsync(idUser).Result!;
    //     ResumeVM resumeVM = _resumeService.GetResume(resumeId, email!);

    //     List<ResumeVM> templatesList = _resumeTemplateService.GetAllResumeTemplates();
    //     TemplateAndResumeVM templateAndResumeVM = new TemplateAndResumeVM
    //     {
    //         Resume = resumeVM,
    //         TemplatesList = templatesList
    //     };
    //     return View(templateAndResumeVM);
    // }

    // [HttpGet("Resume/YourResume/{resumeId}")]
    // public IActionResult YourResume(int resumeId)
    // {
    //     string id = _userManager.GetUserId(User)!;
    //     if (id == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
    //     Resume userResume = _resumeRepository.GetAll().Where(x => x.Id == resumeId).FirstOrDefault()!;
    //     if (userResume == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     if (userResume.UserInfoId != currUser.Id)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     ResumeVM resumeVM = _resumeService.GetResumeHtml(resumeId);

    //     List<ResumeVM> templatesList = _resumeTemplateService.GetAllResumeTemplates();
    //     TemplateAndResumeVM templateAndResumeVM = new TemplateAndResumeVM
    //     {
    //         Resume = resumeVM,
    //         TemplatesList = templatesList
    //     };
    //     return View(templateAndResumeVM);
    // }

    // public IActionResult PreviewResume(int currentResumeId, int templateId)
    // {
    //     string id = _userManager.GetUserId(User)!;
    //     if (id == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
    //     Resume userResume = _resumeRepository.GetAll().Where(x => x.Id == currentResumeId).FirstOrDefault()!;
    //     if (userResume == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     if (userResume.UserInfoId != currUser.Id)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     ResumeVM resumeVM = _resumeService.GetResumeHtml(currentResumeId);
    //     List<ResumeVM> templatesList = _resumeTemplateService.GetAllResumeTemplates();
    //     ResumeVM resumeTemplateVM = _resumeTemplateService.GetResumeTemplateHtml(templateId);
    //     ResumeVM previewResume = _resumeTemplateService.ConvertResumeToTemplate(resumeTemplateVM, resumeVM, currUser);

    //     TemplateAndResumeVM templateAndResumeVM = new TemplateAndResumeVM
    //     {
    //         Resume = resumeVM,
    //         Template = previewResume,
    //         TemplatesList = templatesList
    //     };
    //     return View(templateAndResumeVM);
    // }
    
    // [HttpGet("Resume/ImproveResume/{resumeId}")]
    // public IActionResult ImproveResume(int resumeId)
    // {
    //     string id = _userManager.GetUserId(User)!;
    //     if (id == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
    //     Resume userResume = _resumeRepository.GetAll().Where(x => x.Id == resumeId).FirstOrDefault()!;
    //     if (userResume == null)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     if (userResume.UserInfoId != currUser.Id)
    //     {
    //         return RedirectToAction("Index", "Home");
    //     }
    //     ResumeVM resumeVM = _resumeService.GetResumeHtml(resumeId);
    //     return View(resumeVM);
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}