using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using ResuMeta.ViewModels;
using NUnit.Framework;

namespace ResuMeta_Tests
{
    public class ApplicationTrackerRepository_Tests
    {
        private static readonly string _seedFile = @"../../../Data/SeedApplicationTracker.sql";
        private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void GetApplicationsByUserId_Returns_Applications_Test(int userId, int expected)
        {
            using ResuMetaDbContext context = _dbHelper.GetContext();
            IApplicationTrackerRepository repo = new ApplicationTrackerRepository(context);

            List<ApplicationTrackerVM> applications = repo.GetApplicationsByUserId(userId);

            Assert.That(applications.Count, Is.EqualTo(expected));
        }

        [TestCase(3)]
        public void GetApplicationsByUserId_Returns_EmptyList_Given_NoMatchingApplications_Test(int userId)
        {
            using ResuMetaDbContext context = _dbHelper.GetContext();
            IApplicationTrackerRepository repo = new ApplicationTrackerRepository(context);

            List<ApplicationTrackerVM> applications = repo.GetApplicationsByUserId(userId);

            Assert.Multiple(() =>
            {
                Assert.That(applications.Count, Is.EqualTo(0));
                Assert.That(applications, Is.Empty);
            });
        }

        [TestCase(4)]
        public void GetApplicationsByUserId_Returns_EmptyList_Given_NoMatchingUserId_Test(int userId)
        {
            using ResuMetaDbContext context = _dbHelper.GetContext();
            IApplicationTrackerRepository repo = new ApplicationTrackerRepository(context);

            List<ApplicationTrackerVM> applications = repo.GetApplicationsByUserId(userId);

            Assert.Multiple(() =>
            {
                Assert.That(applications.Count, Is.EqualTo(0));
                Assert.That(applications, Is.Empty);
            });
        }
    }
}