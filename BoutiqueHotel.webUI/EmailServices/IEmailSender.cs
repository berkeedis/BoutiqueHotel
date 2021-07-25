using System.Threading.Tasks;

namespace BoutiqueHotel.webUI.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}