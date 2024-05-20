using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta_Tests;

public class VoteRepository_Tests
{
    private static readonly string _seedFile = @"../../../Data/SeedProfiles.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    [TestCase(1, 1)]
    [TestCase(2, 0)]
    public void GetAllUpVotesByResumeId_Returns_Correct_Amount_Of_Upvotes_Test(int resumeId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        List<UserVote> upvotes = repo.GetAllUpVotesByResumeId(resumeId);

        Assert.That(upvotes.Count, Is.EqualTo(expected));
    }

    [TestCase(0)]
    public void GetAllUpVotesByResumeId_Returns_EmptyList_With_Invalid_ResumeId_Test(int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        List<UserVote> upvotes = repo.GetAllUpVotesByResumeId(null);

        Assert.That(upvotes.Count, Is.EqualTo(expected));
    }

    [TestCase(1, 1)]
    [TestCase(2, 0)]
    public void GetAllDownVotesByResumeId_Returns_Correct_Amount_Of_Downvotes_Test(int resumeId, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        List<UserVote> upvotes = repo.GetAllDownVotesByResumeId(resumeId);

        Assert.That(upvotes.Count, Is.EqualTo(expected));
    }

    [TestCase(0)]
    public void GetAllDownVotesByResumeId_Returns_EmptyList_With_Invalid_ResumeId_Test(int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        List<UserVote> upvotes = repo.GetAllDownVotesByResumeId(null);

        Assert.That(upvotes.Count, Is.EqualTo(expected));
    }

    [TestCase("UP", 1)]
    [TestCase("DOWN", 2)]
    public void GetVoteIdByValue_Returns_Correct_VoteId_Test(string voteValue, int expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        int id = repo.GetVoteIdByValue(voteValue);

        Assert.That(id, Is.EqualTo(expected));
    }

    [TestCase("INVALID")]
    public void GetVoteIdByValue_Throws_Exception_With_Invalid_VoteValue_Test(string voteValue)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        Assert.Throws<Exception>(() => repo.GetVoteIdByValue(voteValue));
    }


    [TestCase(3, 1, 1, 1)]
    public void AddOrUpdateVote_Throws_Exception_If_User_Votes_On_Their_Own_Resume_Test(int Id, int ResumeId, int UserInfoId, int VoteId)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        IVoteRepository repo = new VoteRepository(context);

        UserVoteVM vote = new UserVoteVM
        {
            Id = Id,
            ResumeId = ResumeId,
            UserInfoId = UserInfoId,
            VoteId = VoteId
        };

        Assert.ThrowsAsync<Exception>(() => repo.AddOrUpdateVote(vote));
    }
}