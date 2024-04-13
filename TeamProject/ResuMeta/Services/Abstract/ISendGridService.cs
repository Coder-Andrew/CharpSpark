using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace ResuMeta.Services.Abstract
{
    public interface ISendGridService
    {
        // Task SendApplicationDeadlineReminder(EmailAddress to, DateTime applicationDate);
        // Task SendFollowUpReminder(EmailAddress to, DateTime appliedDate);
        Task TestReminder2(string userId);
    }
}