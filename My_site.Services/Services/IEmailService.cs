
namespace My_site.Services.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body, bool isHtml = true);
    }
}