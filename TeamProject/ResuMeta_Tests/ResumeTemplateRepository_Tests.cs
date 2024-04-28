using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class ResumeTemplateRepository_Tests
{
    // private static readonly string _seedFile = @"..\..\..\Data\SeedResumes.sql";
    private static readonly string _seedFile = @"../../../Data/SeedResumeTEmplates.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(5)]
    public void GetAllResumeTemplates_Returns_ResumeTemplates_Test(int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeTemplateRepository repo = new ResumeTemplateRepository(context);

        List<ResumeVM> resumeTemplates = repo.GetAllResumeTemplates();

        Assert.That(resumeTemplates.Count, Is.EqualTo(expected));
    }

    [TestCase(1, "Resume Template - 1")]
    [TestCase(2, "Resume Template - 2")]
    [TestCase(3, "Resume Template - 3")]
    [TestCase(4, "Resume Template - 4")]
    [TestCase(5, "Resume Template - 5")]
    public void GetResumeTemplateHtml_ReturnsCorrectResumeVM_WithValidInfo_Test(int resumeTemplateId, string title)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeTemplateRepository repo = new ResumeTemplateRepository(context);

        ResumeVM resumeTemplate = repo.GetResumeTemplateHtml(resumeTemplateId);

        Assert.Multiple(() =>
        {
            Assert.That(resumeTemplate.Title, Is.EqualTo($"Resume Template - {resumeTemplateId}"));
        });
    }

    [TestCase(100)]
    [TestCase(200)]
    [TestCase(300)]
    public void  GetResumeTemplateHtml_ReturnsException_WithInvalidResumeTemplateId_Test(int resumeTemplateId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IResumeTemplateRepository repo = new ResumeTemplateRepository(context);

        Assert.Throws<Exception>(() => repo.GetResumeTemplateHtml(resumeTemplateId));
    }

}