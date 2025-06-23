using ResuMeta.Services.Abstract;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Concrete
{
    public class FakeSendGridService : ISendGridService
    {
        public Task SendEmailReminderToApply(ReminderVM reminder, string currUserId)
        {
            return new Task(() => { });
        }

        public Task SendEmailReminderToFollowUp(ReminderVM reminder, string currUserId)
        {
            return new Task(() => { });
        }
    }
}
