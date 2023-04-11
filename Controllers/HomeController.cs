using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using System.Net;
using System.Net.Mail;

namespace Application.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IAntiforgery _antiforgery;

    public HomeController
    (
        IConfiguration configuration,
        IAntiforgery antiforgery
    )
    {
        _configuration = configuration;
        _antiforgery = antiforgery;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index() => RedirectToAction("Blog");

    [HttpGet]
    [Route("/blog")]
    public IActionResult Blog() => View();

    [HttpGet]
    [Route("/projects")]
    public IActionResult Projects() => View();

    [HttpGet]
    [Route("/about")]
    public IActionResult About() => View();

    [HttpGet]
    [Route("/contact")]
    public IActionResult Contact() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/send-email")]
    public IActionResult SendEmail(EmailViewModel emailViewModel)
    {
        if (ModelState.IsValid)
        {
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];
            var server = _configuration["EmailSettings:Server"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);

            var clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            MailMessage message = new MailMessage();

            message.From = new MailAddress(email);
            message.To.Add(email);
            message.Subject = $"Ramon's Porfolio - {emailViewModel.MessageSubject}";

            message.Body = $"Name:\n{emailViewModel.SenderName}\n\n" +
                  $"Email:\n{emailViewModel.SenderEmail}\n\n" +
                  $"Message:\n{emailViewModel.MessageBody}\n\n" +
                  $"Message Timestamp:\n{DateTime.UtcNow}\n\n" +
                  $"IP Address:\n{clientIpAddress}";

            SmtpClient smtp = new SmtpClient(server, port);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(email, password);
            smtp.EnableSsl = true;
            smtp.Send(message);

            return RedirectToAction("EmailSent");
        }

        return View();
    }

    [HttpGet]
    [Route("/email-sent")]
    public IActionResult EmailSent() => View();

    [HttpGet]
    [Route("/skills")]
    public IActionResult Skills() => View();

    [HttpGet]
    [Route("/error")]
    public IActionResult Error() => View();
}