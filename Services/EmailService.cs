using Microsoft.Extensions.Configuration;
using ServiceInterfaces;
using System.Net;
using System.Net.Mail;

namespace Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _senderEmail;
    private readonly string _senderName;

    public EmailService(IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection("SmtpSettings");

        _senderEmail = smtpSettings["SenderEmail"] ?? throw new ArgumentNullException("SenderEmail is not configured in appsettings.");
        _senderName = smtpSettings["SenderName"] ?? throw new ArgumentNullException("SenderName is not configured in appsettings.");
        string smtpServer = smtpSettings["Server"] ?? throw new ArgumentNullException("Server is not configured in appsettings.");
        string smtpPort = smtpSettings["Port"] ?? throw new ArgumentNullException("Port is not configured in appsettings.");
        string smtpUsername = smtpSettings["Username"] ?? throw new ArgumentNullException("Username is not configured in appsettings.");
        string smtpPassword = smtpSettings["Password"] ?? throw new ArgumentNullException("Password is not configured in appsettings.");

        _smtpClient = new SmtpClient(smtpServer)
        {
            Port = int.Parse(smtpPort),
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true,
        };
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_senderEmail, _senderName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}
