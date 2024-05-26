using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class ProfileViewsRepository : Repository<ProfileViews>, IProfileViewsRepository
    {
        private readonly DbSet<ProfileViews> _views;
        private readonly ResuMetaDbContext _context;
        public ProfileViewsRepository(ResuMetaDbContext context) : base(context)
        {
            _views = context.ProfileViews;
            _context = context;
        }


        public int? GetViewCount(int? profileId) {
            if (profileId == null)
            {
                throw new Exception("Profile ID cannot be null");
            }
            ProfileViews view = _views.FirstOrDefault(v => v.ProfileId == profileId)!;
            if (view == null)
            {
                throw new Exception("Profile not found");
            }
            int? viewCount = view.ViewCount;
            if (viewCount == null)
            {
                return 0;
            }
            return viewCount;
        }

        public async Task UpdateViewCount(int? profileId) {
            if (profileId == null)
            {
                throw new Exception("Profile ID cannot be null");
            }
            ProfileViews view = _views.FirstOrDefault(v => v.ProfileId == profileId)!;
            if (view == null)
            {
                throw new Exception("Profile not found");
            }
            view.ViewCount++;
            _views.Update(view);
            await _context.SaveChangesAsync();
        }
    }
}
