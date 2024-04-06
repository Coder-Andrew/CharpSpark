using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class CoverLetterRepository_Tests
{
    // private static readonly string _seedFile = @"..\..\..\Data\SeedResumes.sql";
    private static readonly string _seedFile = @"../../../Data/SeedCoverLetters.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(1, 2)]
    [TestCase(2, 2)]
    public void GetAllCoverLetters_Returns_CoverLetters_Test(int userId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        List<CoverLetterVM> coverLetters = repo.GetAllCoverLetters(userId);

        Assert.That(coverLetters.Count, Is.EqualTo(expected));
    }

    [TestCase(3)]
    public void GetAllCoverLetters_Returns_EmptyList_Given_NoMatchingCoverLetters_Test(int userId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        List<CoverLetterVM> coverLetters = repo.GetAllCoverLetters(userId);

        Assert.Multiple(() =>
        {
            Assert.That(coverLetters.Count, Is.EqualTo(0));
            Assert.That(coverLetters, Is.Empty);
        });
    }

    [TestCase(4)]
    public void GetAllCoverLetters_Returns_EmptyList_Given_NoMatchingUserId_Test(int userId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        List<CoverLetterVM> coverLetters = repo.GetAllCoverLetters(userId);

        Assert.Multiple(() =>
        {
            Assert.That(coverLetters.Count, Is.EqualTo(0));
            Assert.That(coverLetters, Is.Empty);
        });
    }

    [TestCase(1, "Cover Letter 1", "%3Cp%3EDear%20Mrs.%20Dawn%2C%3C%2Fp%3E%3Cp%3ETest%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EJasmine%20Patel%3C%2Fp%3E")]
    [TestCase(2, "Cover Letter 2", "%3Cp%3EDear%20Ms.%20Dawn%2C%3C%2Fp%3E%3Cp%3EThis%20is%20just%20a%20test%20cover%20letter%20body.%20It%20is%20written%20to%20be%20a%20longer%20length%20since%20a%20cover%20letter%20is%20likely%20to%20be%20on%20the%20longer%20side%2C%20considering%20you%20are%20writing%20out%20why%20you're%20a%20perfect%20fit%20for%20the%20position.%20I%20believe%20that%20I'm%20a%20perfect%20fit%20for%20this%20position.%20I've%20worked%20several%20jobs%20in%20my%20lifetime%20and%20have%20acquired%20a%20lot%20of%20skills%20that%20are%20specific%20to%20this%20job.%20I%20am%20excited%20to%20hear%20back.%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EJasmine%20Patel%3C%2Fp%3E")]
    [TestCase(5, "Cover Letter 5", "%3Cp%3EDear%20Mr.%20Smith%2C%3C%2Fp%3E%3Cp%3ETest%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EAdrian%20Reynolds%3C%2Fp%3E")]
    public void GetCoverLetterHtml_ReturnsCorrectCoverLetterVM_WithValidInfo_Test(int coverLetterId, string title, string htmlContent)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        CoverLetterVM coverLetter = repo.GetCoverLetterHtml(coverLetterId);

        Assert.Multiple(() =>
        {
            Assert.That(coverLetter.CoverLetterId, Is.EqualTo(coverLetterId));
            Assert.That(coverLetter.Title, Is.EqualTo(title));
            Assert.That(coverLetter.HtmlContent, Is.EqualTo(htmlContent));
        });
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void GetCoverLetterHtml_ReturnsException_WithInvalidCoverLetterId_Test(int coverLetterId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        Assert.Throws<Exception>(() => repo.GetCoverLetterHtml(coverLetterId));
    }

    [TestCase(1)]
    public void GetCoverLetter_ReturnsCorrectCoverLetterVM_ForCoverLetter1_Test(int coverLetterId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        CoverLetterVM coverLetter = repo.GetCoverLetter(coverLetterId);

        Assert.Multiple(() =>
        {
            Assert.That(coverLetter.CoverLetterId, Is.EqualTo(coverLetterId));
            Assert.That(coverLetter.FirstName, Is.EqualTo("Jasmine"));
            Assert.That(coverLetter.LastName, Is.EqualTo("Patel"));
            Assert.That(coverLetter.Title, Is.EqualTo("Cover Letter 1"));
            Assert.That(coverLetter.HiringManager, Is.EqualTo("Mrs. Dawn"));
            Assert.That(coverLetter.Body, Is.EqualTo("Test"));
        });
    }

    [TestCase(4)]
    public void GetCoverLetter_ReturnsCorrectCoverLetterVM_ForCoverLetter4_Test(int coverLetterId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        CoverLetterVM coverLetter = repo.GetCoverLetter(coverLetterId);

        Assert.Multiple(() =>
        {
            Assert.That(coverLetter.CoverLetterId, Is.EqualTo(coverLetterId));
            Assert.That(coverLetter.FirstName, Is.EqualTo("Adrian"));
            Assert.That(coverLetter.LastName, Is.EqualTo("Reynolds"));
            Assert.That(coverLetter.Title, Is.EqualTo("Cover Letter 4"));
            Assert.That(coverLetter.HiringManager, Is.EqualTo("Mr. Smith"));
            Assert.That(coverLetter.Body, Is.EqualTo("Testing"));
        });
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void GetCoverLetter_ReturnsException_WithInvalidCoverLetterId(int coverLetterId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ICoverLetterRepository repo = new CoverLetterRepository(context);

        Assert.Throws<Exception>(() => repo.GetCoverLetter(coverLetterId));
    }
}