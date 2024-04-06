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
}