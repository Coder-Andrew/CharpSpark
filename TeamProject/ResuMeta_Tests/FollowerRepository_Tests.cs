using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class FollowerRepository_Tests
{
    private static readonly string _seedFile = @"../../../Data/SeedProfiles.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(6, 0)]
    public void GetFollowersByProfileId_Returns_Correct_Amount_Of_Followers_Test(int profileId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IFollowerRepository repo = new FollowerRepository(context);

        List<Follower> result = repo.GetFollowersByProfileId(profileId);

        Assert.That(result.Count, Is.EqualTo(expected));
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(6, 0)]
    public void GetFollowingByProfileId_Returns_Correct_Amount_Of_Followers_Test(int profileId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IFollowerRepository repo = new FollowerRepository(context);

        List<Follower> result = repo.GetFollowingByProfileId(profileId);

        Assert.That(result.Count, Is.EqualTo(expected));
    }

    [TestCase(null)]
    public void GetFollowersByProfileId_Throws_Exception_With_Invalid_ProfileId_Test(int? profileId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IFollowerRepository repo = new FollowerRepository(context);

        Assert.Throws<Exception>(() => repo.GetFollowersByProfileId(profileId));
    }

    [TestCase(null)]
    public void GetFollowingByProfileId_Throws_Exception_With_Invalid_ProfileId_Test(int? profileId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IFollowerRepository repo = new FollowerRepository(context);

        Assert.Throws<Exception>(() => repo.GetFollowingByProfileId(profileId));
    }

}