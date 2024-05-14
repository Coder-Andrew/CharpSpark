using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class ProfileRepository_Tests
{
    private static readonly string _seedFile = @"../../../Data/SeedProfiles.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(1, 1, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "reynoldsa@mail.com", "Adrian", "Reynolds", null, "Summary Here")]
    [TestCase(2, 2, "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E", "patelj@mail.com", "Jasmine", "Patel", null, "Summary Here")]
    public void GetProfileById_Returns_Correct_ViewModels_Test(int userId, int profileId, string resume, string userName, string firstName, string lastName, byte[] profilePic, string description)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IProfileRepository repo = new ProfileRepository(context);

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

        ProfileVM profile = repo.GetProfileById(userId, userName, firstName, lastName, profilePic);

        Assert.Multiple( () =>
            {
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

    [TestCase(3, "mitchelle@mail.com", "Emily", "Mitchell", null)]
    public void GetProfileById_Throws_Exception_With_Invalid_ProfileId_Test(int userId, string userName, string firstName, string lastName, byte[] profilePic)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IProfileRepository repo = new ProfileRepository(context);

        Assert.Throws<Exception>(() => repo.GetProfileById(userId, userName, firstName, lastName, profilePic));
    }

}