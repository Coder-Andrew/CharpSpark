using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IFollowerService
    {
        Task<List<ProfileVM>> GetFollowersByProfileId(int? profileId);
        Task<List<ProfileVM>> GetFollowingByProfileId(int? profileId);
        void AddFollower(int? profileId, int? followerId);
        void RemoveFollower(int? profileId, int? followerId);
        int GetFollowerCount(int? profileId);
        int GetFollowingCount(int? profileId);

    }
}