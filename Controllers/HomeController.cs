using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Antiforgery;

namespace Application.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IAntiforgery _antiforgery;

    public HomeController(IConfiguration configuration, IAntiforgery antiforgery)
    {
        _configuration = configuration;
        _antiforgery = antiforgery;
    }

    public IActionResult Index() => RedirectToAction("Blog");

    [Route("/blog")]
    public IActionResult Blog() => View();

    [Route("/projects")]
    public IActionResult Projects() => View();

    [Route("/about")]
    public IActionResult About() => View();

    [Route("/contact")]
    public IActionResult Contact() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/sendEmail")]
    public IActionResult SendEmail(EmailViewModel? emailViewModel)
    {
        emailViewModel.MessageTimestamp = DateTime.UtcNow;

        if (ModelState.IsValid)
        {
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];
            var server = _configuration["EmailSettings:Server"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);

            MailMessage message = new MailMessage();

            message.From = new MailAddress(email);
            message.To.Add(email);
            message.Subject = emailViewModel.MessageSubject;
            message.Body = $"Name: {emailViewModel.SenderName}\nEmail: {emailViewModel.SenderEmail}\nMessage: {emailViewModel.MessageBody}\nMessage Timestamp: {emailViewModel.MessageTimestamp}";

            SmtpClient smtp = new SmtpClient(server, port);
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(email, password);

            smtp.EnableSsl = true;
            smtp.Send(message);
            return View(new { emailSent = true });
        }

        return View(new { emailSent = false });
    }

    [Route("/skills")]
    public IActionResult Skills() => View();

    [Route("/error")]
    public IActionResult Error() => View();
}