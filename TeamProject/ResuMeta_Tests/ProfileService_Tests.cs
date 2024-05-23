using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;
using ResuMeta.Data;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Services.Abstract;
using ResuMeta.Services.Concrete;

namespace ResuMeta_Tests;

public class ProfileService_Tests
{
    private static readonly string _seedFile = @"../../../Data/SeedProfiles.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);


    [TestCase(1, 1, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    [TestCase(2, 2, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "patelj@mail.com", "Jasmine", "Patel", null, "Summary Here")]
    public async Task GetProfile_Returns_Correct_ViewModels_Test(int profileId, int userId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);


        ProfileVM expected = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = description,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        ProfileVM profile = await profileService.GetProfile(profileId);

        Assert.Multiple( () =>
            {
                Assert.That(profile.ProfileId, Is.EqualTo(expected.ProfileId));
                Assert.That(profile.Resume, Is.EqualTo(expected.Resume));
                Assert.That(profile.Description, Is.EqualTo(expected.Description));
                Assert.That(profile.UserName, Is.EqualTo(expected.UserName));
                Assert.That(profile.FirstName, Is.EqualTo(expected.FirstName));
                Assert.That(profile.LastName, Is.EqualTo(expected.LastName));
                Assert.That(profile.ProfilePicturePath, Is.EqualTo(expected.ProfilePicturePath));
                Assert.That(profile.FollowerCount, Is.EqualTo(1));
                Assert.That(profile.FollowingCount, Is.EqualTo(1));
            }
        );
    }

    [TestCase(3, 1, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    [TestCase(4, 2, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "patelj@mail.com", "Jasmine", "Patel", null, "Summary Here")]
    public async Task GetProfile_Throws_Exception_With_Invalid_ProfileId_Test(int profileId, int userId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);


        ProfileVM expected = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = description,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        try{
            await profileService.GetProfile(profileId);
            Assert.Fail("No exception thrown");
        }
        catch(Exception e)
        {
            Assert.That(e.Message, Is.EqualTo("Profile not found"));
        }
    }

    [TestCase(5, "reynoldsa@mail.com")]
    public async Task GetProfile_Throws_Exception_With_Invalid_UserInfoId_Test(int profileId, string userName)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        try{
            await profileService.GetProfile(profileId);
            Assert.Fail("No exception thrown");
        }
        catch(Exception e)
        {
            Assert.That(e.Message, Is.EqualTo("User not found"));
        }
    }

    [TestCase(6)]
    public async Task GetProfile_Throws_Exception_With_Invalid_AspNetUser_Test(int profileId)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        ApplicationUser? user = null;
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        try{
            await profileService.GetProfile(profileId);
            Assert.Fail("No exception thrown");
        }
        catch(Exception e)
        {
            Assert.That(e.Message, Is.EqualTo("User not found"));
        }
    }

    [TestCase(1, 1, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    [TestCase(2, 2, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "patelj@mail.com", "Jasmine", "Patel", null, "Summary Here")]
    public async Task SaveProfile_Returns_True_And_Saves_Profile_Test(int profileId, int userId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);


        ProfileVM newProfile = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = "New description",
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        ProfileVM expected = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = "New description",
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        bool result = profileService.SaveProfile(userId,  newProfile);
        ProfileVM profile = await profileService.GetProfile(profileId);

        Assert.Multiple( () =>
            {
                Assert.That(result, Is.EqualTo(true));
                Assert.That(profile.ProfileId, Is.EqualTo(expected.ProfileId));
                Assert.That(profile.Resume, Is.EqualTo(expected.Resume));
                Assert.That(profile.Description, Is.EqualTo(expected.Description));
                Assert.That(profile.UserName, Is.EqualTo(expected.UserName));
                Assert.That(profile.FirstName, Is.EqualTo(expected.FirstName));
                Assert.That(profile.LastName, Is.EqualTo(expected.LastName));
                Assert.That(profile.ProfilePicturePath, Is.EqualTo(expected.ProfilePicturePath));
            }
        );
    }

    [TestCase(5, null!, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    public void SaveProfile_Returns_False_With_Invalid_UserInfoId_Test(int profileId, int? userId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        ProfileVM newProfile = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = "New description",
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        bool result = profileService.SaveProfile(22,  newProfile);

        Assert.That(result, Is.EqualTo(false));
    }

    [TestCase(80, 3, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    public void SaveProfile_Returns_False_With_Invalid_Profile_Test(int profileId, int? userId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user = new ApplicationUser { UserName = userName, Email = userName };
        mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        ProfileVM newProfile = new ProfileVM
        {
            ProfileId = profileId,
            Resume = resume,
            Description = "New description",
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePicturePath = profilePic
        };

        bool result = profileService.SaveProfile(3,  newProfile);

        Assert.That(result, Is.EqualTo(false));
    }

    [TestCase("mail", 2)]
    [TestCase("rey", 1)]
    [TestCase("adr", 1)]
    [TestCase("pat", 1)]
    [TestCase("jasmine", 1)]
    [TestCase("emily", 0)]
    public async Task SearchProfiles_Returns_Correct_Count_Of_Profiles_Test(string keyword, int expected)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user1 = new ApplicationUser { UserName = "reynoldsa@mail.com", Email = "reynoldsa@mail.com" };
        var user2 = new ApplicationUser { UserName = "patelj@mail.com", Email = "patelj@mail.com" };
        mockUserManager.Setup(um => um.FindByIdAsync("1")).ReturnsAsync(user1);
        mockUserManager.Setup(um => um.FindByIdAsync("2")).ReturnsAsync(user2);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        List<ProfileVM> result = await profileService.SearchProfile(keyword);

        Assert.That(result.Count(), Is.EqualTo(expected));
    }

    [TestCase("mail", 1, 1, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here", 2, 2, "patelj@mail.com", "Jasmine", "Patel")]
    public async Task SearchProfiles_Returns_Correct_List_Of_ProfileVMS_Test(string keyword, int profile1Id, int user1Id, string resume, string userName1, string firstName1, string lastName1, byte[] profilePic, string description, int profile2Id, int user2Id, string userName2, string firstName2, string lastName2)
    {
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Set up the mock UserManager to return a specific user when FindByIdAsync is called
        var user1 = new ApplicationUser { UserName = "reynoldsa@mail.com", Email = "reynoldsa@mail.com" };
        var user2 = new ApplicationUser { UserName = "patelj@mail.com", Email = "patelj@mail.com" };
        mockUserManager.Setup(um => um.FindByIdAsync("1")).ReturnsAsync(user1);
        mockUserManager.Setup(um => um.FindByIdAsync("2")).ReturnsAsync(user2);


        using ResuMetaDbContext context = _dbHelper.GetContext();
        IUserInfoRepository userRepo = new UserInfoRepository(context);
        IProfileRepository repo = new ProfileRepository(context);
        IRepository<Profile> profileRepo = new Repository<Profile>(context);
        IVoteRepository voteRepo = new VoteRepository(context);
        IFollowerRepository followerRepo = new FollowerRepository(context);

        IProfileService profileService = new ProfileService(null!, mockUserManager.Object, userRepo, repo, profileRepo, voteRepo, followerRepo);

        ProfileVM profile1 = new ProfileVM
        {
            ProfileId = profile1Id,
            Resume = resume,
            Description = description,
            UserName = userName1,
            FirstName = firstName1,
            LastName = lastName1,
            ProfilePicturePath = profilePic
        };

        ProfileVM profile2 = new ProfileVM
        {
            ProfileId = profile2Id,
            Resume = resume,
            Description = description,
            UserName = userName2,
            FirstName = firstName2,
            LastName = lastName2,
            ProfilePicturePath = profilePic
        };


        List<ProfileVM> result = await profileService.SearchProfile(keyword);

        Assert.Multiple(() =>
        {
            Assert.That(result[0].ProfileId, Is.EqualTo(profile1.ProfileId));
            Assert.That(result[1].ProfileId, Is.EqualTo(profile2.ProfileId));
            Assert.That(result[0].Resume, Is.EqualTo(profile1.Resume));
            Assert.That(result[1].Resume, Is.EqualTo(profile2.Resume));
            Assert.That(result[0].Description, Is.EqualTo(profile1.Description));
            Assert.That(result[1].Description, Is.EqualTo(profile2.Description));
            Assert.That(result[0].UserName, Is.EqualTo(profile1.UserName));
            Assert.That(result[1].UserName, Is.EqualTo(profile2.UserName));
            Assert.That(result[0].FirstName, Is.EqualTo(profile1.FirstName));
            Assert.That(result[1].FirstName, Is.EqualTo(profile2.FirstName));
            Assert.That(result[0].LastName, Is.EqualTo(profile1.LastName));
            Assert.That(result[1].LastName, Is.EqualTo(profile2.LastName));
            Assert.That(result[0].ProfilePicturePath, Is.EqualTo(profile1.ProfilePicturePath));
            Assert.That(result[1].ProfilePicturePath, Is.EqualTo(profile2.ProfilePicturePath));
        });
        
    }
}