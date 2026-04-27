namespace LuminiSchool.Infrastructure.Email.Contract
{
    public interface IEmailService
    {
        Task SendTemporaryPasswordAsync(string toEmail, string fullName, string temporaryPassword);
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}
