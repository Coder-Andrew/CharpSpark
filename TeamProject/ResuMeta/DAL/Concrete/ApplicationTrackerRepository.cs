using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class ApplicationTrackerRepository : Repository<ApplicationTracker>, IApplicationTrackerRepository
    {
        private readonly DbSet<ApplicationTracker> _applicationTrackers;
        public ApplicationTrackerRepository(ResuMetaDbContext context) : base(context)
        {
            _applicationTrackers = context.ApplicationTrackers;
        }

        public List<ApplicationTrackerVM> GetApplicationsByUserId(int userId)
        {
            return _applicationTrackers
                .Where(a => a.UserInfoId == userId)
                .Select(a => new ApplicationTrackerVM
                {
                    ApplicationTrackerId = a.Id,
                    JobTitle = a.JobTitle,
                    CompanyName = a.CompanyName,
                    JobListingUrl = a.JobListingUrl,
                    AppliedDate = a.AppliedDate,
                    ApplicationDeadline = a.ApplicationDeadline,
                    Status = a.Status,
                    Notes = a.Notes
                })
                .ToList();
        }


    }
}