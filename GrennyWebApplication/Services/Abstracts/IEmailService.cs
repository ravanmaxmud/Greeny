using GrennyWebApplication.Contracts.Email;

namespace GrennyWebApplication.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
