using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class ResumeRepository_Tests
{
    // private static readonly string _seedFile = @"..\..\..\Data\SeedResumes.sql";
    private static readonly string _seedFile = @"../../../Data/SeedResumes.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(1, 2)]
    [TestCase(2, 2)]
    public void GetAllResumes_Returns_Resumes_Test(int userId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        List<ResumeVM> resumes = repo.GetAllResumes(userId);

        Assert.That(resumes.Count, Is.EqualTo(expected));
    }

    [TestCase(3)]
    public void GetAllResumes_Returns_EmptyList_Given_NoMatchingResumes_Test(int userId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        List<ResumeVM> resumes = repo.GetAllResumes(userId);

        Assert.Multiple(() =>
        {
            Assert.That(resumes.Count, Is.EqualTo(0));
            Assert.That(resumes, Is.Empty);
        });
    }

    [TestCase(4)]
    public void GetAllResumes_Returns_EmptyList_Given_NoMatchingUserId_Test(int userId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        List<ResumeVM> resumes = repo.GetAllResumes(userId);

        Assert.Multiple(() =>
        {
            Assert.That(resumes.Count, Is.EqualTo(0));
            Assert.That(resumes, Is.Empty);
        });
    }

    [TestCase(1, "Resume 1", "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E")]
    [TestCase(2, "Resume 2", "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E")]
    [TestCase(4, "Resume 4", "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E")]
    [TestCase(5, "Resume 5", "%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E")]
    public void GetResumeHtml_ReturnsCorrectResumeVM_WithValidInfo_Test(int resumeId, string title, string htmlContent)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        ResumeVM resume = repo.GetResumeHtml(resumeId);

        Assert.Multiple(() =>
        {
            Assert.That(resume.ResumeId, Is.EqualTo(resumeId));
            Assert.That(resume.Title, Is.EqualTo(title));
            Assert.That(resume.HtmlContent, Is.EqualTo(htmlContent));
        });
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void GetResumeHtml_ReturnsException_WithInvalidResumeId_Test(int resumeId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        Assert.Throws<Exception>(() => repo.GetResumeHtml(resumeId));
    }

    [TestCase(1, "reynoldsa@mail.com")]
    public void GetResume_ReturnsCorrectResumeVM_ForResume1_Test(int resumeId, string email)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        ResumeVM resume = repo.GetResume(resumeId, email);

        Assert.Multiple(() =>
        {
            Assert.That(resume.ResumeId, Is.EqualTo(resumeId));
            Assert.That(resume.Email, Is.EqualTo(email));
            Assert.That(resume.FirstName, Is.EqualTo("Adrian"));
            Assert.That(resume.LastName, Is.EqualTo("Reynolds"));
            Assert.That(resume.Phone, Is.EqualTo("555-628-1234"));
            Assert.That(resume.Education.Count, Is.EqualTo(1));
            Assert.That(resume.Education[0].Institution, Is.EqualTo("WOU"));
            Assert.That(resume.Education[0].EducationSummary, Is.EqualTo("Summary Here"));
            Assert.That(resume.Education[0].StartDate, Is.EqualTo(DateOnly.Parse("2024-01-01")));
            Assert.That(resume.Education[0].EndDate, Is.EqualTo(DateOnly.Parse("2024-03-01")));
            Assert.That(resume.Education[0].Completion, Is.True);
            Assert.That(resume.Degree.Count, Is.EqualTo(1));
            Assert.That(resume.Degree[0].Type, Is.EqualTo("Bachelor"));
            Assert.That(resume.Degree[0].Major, Is.EqualTo("CS"));
            Assert.That(resume.Degree[0].Minor, Is.EqualTo("IS"));
            Assert.That(resume.Skills.Count, Is.EqualTo(2));
            Assert.That(resume.Skills[0].SkillName, Is.EqualTo("Python"));
            Assert.That(resume.Skills[1].SkillName, Is.EqualTo("Java"));
            Assert.That(resume.Achievements.Count, Is.EqualTo(1));
            Assert.That(resume.Achievements[0].title, Is.EqualTo("Honor Roll"));
            Assert.That(resume.Achievements[0].summary, Is.EqualTo("4.0 GPA"));
            Assert.That(resume.Projects.Count, Is.EqualTo(1));
            Assert.That(resume.Projects[0].Name, Is.EqualTo("resuMeta"));
            Assert.That(resume.Projects[0].Link, Is.EqualTo("https://www.github.com/Coder-Andrew/CharpSpark"));
            Assert.That(resume.Projects[0].Summary, Is.EqualTo("Senior Sequence Project"));
        });
    }

    [TestCase(2, "reynoldsa@mail.com")]
    public void GetResume_ReturnsCorrectResumeVM_ForResume2_Test(int resumeId, string email)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        ResumeVM resume = repo.GetResume(resumeId, email);

        Assert.Multiple(() =>
        {
            Assert.That(resume.ResumeId, Is.EqualTo(resumeId));
            Assert.That(resume.Email, Is.EqualTo(email));
            Assert.That(resume.FirstName, Is.EqualTo("Adrian"));
            Assert.That(resume.LastName, Is.EqualTo("Reynolds"));
            Assert.That(resume.Phone, Is.EqualTo("555-628-1234"));
            Assert.That(resume.Education.Count, Is.EqualTo(1));
            Assert.That(resume.Education[0].Institution, Is.EqualTo("WOU"));
            Assert.That(resume.Education[0].EducationSummary, Is.EqualTo("Summary Here"));
            Assert.That(resume.Education[0].StartDate, Is.EqualTo(DateOnly.Parse("2024-01-01")));
            Assert.That(resume.Education[0].EndDate, Is.EqualTo(DateOnly.Parse("2024-03-01")));
            Assert.That(resume.Education[0].Completion, Is.True);
            Assert.That(resume.Degree.Count, Is.EqualTo(1));
            Assert.That(resume.Degree[0].Type, Is.EqualTo("Bachelor"));
            Assert.That(resume.Degree[0].Major, Is.EqualTo("Computer Science"));
            Assert.That(resume.Degree[0].Minor, Is.EqualTo("Information Systems"));
            Assert.That(resume.Skills.Count, Is.EqualTo(1));
            Assert.That(resume.Skills[0].SkillName, Is.EqualTo("JavaScript"));
            Assert.That(resume.Achievements.Count, Is.EqualTo(1));
            Assert.That(resume.Achievements[0].title, Is.EqualTo("Honor Roll"));
            Assert.That(resume.Achievements[0].summary, Is.EqualTo("4.0 GPA"));
            Assert.That(resume.Projects.Count, Is.EqualTo(1));
            Assert.That(resume.Projects[0].Name, Is.EqualTo("DD&BB"));
            Assert.That(resume.Projects[0].Link, Is.EqualTo("localhost:xxxx"));
            Assert.That(resume.Projects[0].Summary, Is.EqualTo("Testing"));
        });
    }

    [TestCase(4, "patelj@mail.com")]
    public void GetResume_ReturnsCorrectResumeVM_ForResume4(int resumeId, string email)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        ResumeVM resume = repo.GetResume(resumeId, email);

        Assert.Multiple(() =>
        {
            Assert.That(resume.ResumeId, Is.EqualTo(resumeId));
            Assert.That(resume.Email, Is.EqualTo(email));
            Assert.That(resume.FirstName, Is.EqualTo("Jasmine"));
            Assert.That(resume.LastName, Is.EqualTo("Patel"));
            Assert.That(resume.Phone, Is.EqualTo("555-628-1234"));
            Assert.That(resume.Education.Count, Is.EqualTo(1));
            Assert.That(resume.Education[0].Institution, Is.EqualTo("WOU"));
            Assert.That(resume.Education[0].EducationSummary, Is.EqualTo("Summary Here"));
            Assert.That(resume.Education[0].StartDate, Is.EqualTo(DateOnly.Parse("2024-01-01")));
            Assert.That(resume.Education[0].EndDate, Is.EqualTo(DateOnly.Parse("2024-03-01")));
            Assert.That(resume.Education[0].Completion, Is.True);
            Assert.That(resume.Degree.Count, Is.EqualTo(1));
            Assert.That(resume.Degree[0].Type, Is.EqualTo("Master"));
            Assert.That(resume.Degree[0].Major, Is.EqualTo("Computer Science"));
            Assert.That(resume.Degree[0].Minor, Is.EqualTo("N/A"));
            Assert.That(resume.Skills.Count, Is.EqualTo(1));
            Assert.That(resume.Skills[0].SkillName, Is.EqualTo("Python"));
            Assert.That(resume.Achievements.Count, Is.EqualTo(1));
            Assert.That(resume.Achievements[0].title, Is.EqualTo("Honor Roll"));
            Assert.That(resume.Achievements[0].summary, Is.EqualTo("4.0 GPA"));
            Assert.That(resume.Projects.Count, Is.EqualTo(1));
            Assert.That(resume.Projects[0].Name, Is.EqualTo("resuMeta"));
            Assert.That(resume.Projects[0].Link, Is.EqualTo("https://www.github.com/Coder-Andrew/CharpSpark"));
            Assert.That(resume.Projects[0].Summary, Is.EqualTo("Senior Sequence Project"));
        });
    }

    [TestCase(5, "patelj@mail.com")]
    public void GetResume_ReturnsCorrectResumeVM_ForResume5(int resumeId, string email)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        ResumeVM resume = repo.GetResume(resumeId, email);

        Assert.Multiple(() =>
        {
            Assert.That(resume.ResumeId, Is.EqualTo(resumeId));
            Assert.That(resume.Email, Is.EqualTo(email));
            Assert.That(resume.FirstName, Is.EqualTo("Jasmine"));
            Assert.That(resume.LastName, Is.EqualTo("Patel"));
            Assert.That(resume.Phone, Is.EqualTo("555-628-1234"));
            Assert.That(resume.Education.Count, Is.EqualTo(1));
            Assert.That(resume.Education[0].Institution, Is.EqualTo("WOU"));
            Assert.That(resume.Education[0].EducationSummary, Is.EqualTo("Summary Here"));
            Assert.That(resume.Education[0].StartDate, Is.EqualTo(DateOnly.Parse("2024-01-01")));
            Assert.That(resume.Education[0].EndDate, Is.EqualTo(DateOnly.Parse("2024-03-01")));
            Assert.That(resume.Education[0].Completion, Is.True);
            Assert.That(resume.Degree.Count, Is.EqualTo(1));
            Assert.That(resume.Degree[0].Type, Is.EqualTo("Bachelor"));
            Assert.That(resume.Degree[0].Major, Is.EqualTo("Computer Science"));
            Assert.That(resume.Degree[0].Minor, Is.EqualTo("Mathematics"));
            Assert.That(resume.Skills.Count, Is.EqualTo(1));
            Assert.That(resume.Skills[0].SkillName, Is.EqualTo("Python"));
            Assert.That(resume.Achievements.Count, Is.EqualTo(1));
            Assert.That(resume.Achievements[0].title, Is.EqualTo("Honor Roll"));
            Assert.That(resume.Achievements[0].summary, Is.EqualTo("4.0 GPA"));
            Assert.That(resume.Projects.Count, Is.EqualTo(1));
            Assert.That(resume.Projects[0].Name, Is.EqualTo("DD&BB"));
            Assert.That(resume.Projects[0].Link, Is.EqualTo("localhost:xxxx"));
            Assert.That(resume.Projects[0].Summary, Is.EqualTo("Testing"));
        });
    }

    [TestCase(100, "exception@mail.com")]
    [TestCase(200, "exception@mail.com")]
    [TestCase(300, "exception@mail.com")]
    public void GetResume_ReturnsException_WithInvalidResumeId(int resumeId, string email)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeRepository repo = new ResumeRepository(context);

        Assert.Throws<Exception>(() => repo.GetResume(resumeId, email));
    }
}