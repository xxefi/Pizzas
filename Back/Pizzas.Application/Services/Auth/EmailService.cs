using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Auth;

public class EmailService : IEmailService
{
    private readonly SmtpSettingsEntity _smtpSettings;

    public EmailService(IOptions<SmtpSettingsEntity> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }
    
    public async Task SendOtpAsync(string email, string otpCode)
    {
        var message = new MailMessage(_smtpSettings.From, email)
        {
            Subject = "OTP",
            Body = otpCode,
            IsBodyHtml = false
        };

        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}