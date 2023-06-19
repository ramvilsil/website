using System.Net;
using System.Net.Mail;

namespace Application.Services;

public class EmailSender
{
    private readonly IConfiguration _configuration;

    private readonly string _email;
    private readonly string _password;
    private readonly string _server;
    private readonly int _port;

    public EmailSender
    (
        IConfiguration configuration
    )
    {
        _configuration = configuration;
        _email = _configuration["EmailSettings:Email"];
        _password = _configuration["EmailSettings:Password"];
        _server = _configuration["EmailSettings:Server"];
        _port = int.Parse(_configuration["EmailSettings:Port"]);
    }

    public async Task SendAsync(string messageBody, string subject)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(_email);
        message.To.Add(_email);
        message.Subject = $"Ramon's Porfolio - {subject}";
        message.Body = messageBody;

        SmtpClient smtp = new SmtpClient(_server, _port);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_email, _password);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(message);
    }
}