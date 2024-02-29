using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class ResumeRepository_Tests
{
    private static readonly string _seedFile = @"..\..\..\Data\SeedResumes.sql";
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
}