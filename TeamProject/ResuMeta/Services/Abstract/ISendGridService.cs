using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace ResuMeta.Services.Abstract
{
    public interface ISendGridService
    {
        Task SendEmailReminderToApply(ReminderVM reminder, string currUserId);
        Task SendEmailReminderToFollowUp(ReminderVM reminder, string currUserId);
    }
}