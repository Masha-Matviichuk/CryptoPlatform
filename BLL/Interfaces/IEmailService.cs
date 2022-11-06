using System.Threading.Tasks;
using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendSignUpConfirmationEmailAsync(MailRequest message);
        Task SendMessageToManagerAsync(CustomerMailRequest message);
    }
}