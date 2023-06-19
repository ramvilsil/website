using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Application.ViewModels;
using Application.Services;

namespace Application.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly IAntiforgery _antiforgery;
    private readonly HttpClient _httpClient;
    private readonly EmailSender _emailSender;

    public HomeController
    (
        IWebHostEnvironment env,
        IConfiguration configuration,
        IAntiforgery antiforgery,
        HttpClient httpClient,
        EmailSender emailSender
    )
    {
        _env = env;
        _configuration = configuration;
        _antiforgery = antiforgery;
        _httpClient = httpClient;
        _emailSender = emailSender;
    }

    private async Task<string> GetClientDetailsAsync()
    {
        string? clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrWhiteSpace(clientIpAddress)) return "Client Details Unavailable.";

        return await _httpClient.GetStringAsync($"http://ip-api.com/json/{clientIpAddress}");
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index() => RedirectToAction(nameof(Blog));

    [HttpGet]
    [Route("/blog")]
    public async Task<IActionResult> Blog()
    {
        //try
        //{
        //    await _emailSender.SendAsync(await GetClientDetailsAsync(), "Website View");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Exception: {ex}");
        //}
        return View();
    }

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
    public async Task<IActionResult> SendEmail(EmailViewModel emailViewModel)
    {
        if (!ModelState.IsValid) return View();

        try
        {
            string messageBody = $"Name:\n{emailViewModel.SenderName}\n\n" +
              $"Email:\n{emailViewModel.SenderEmail}\n\n" +
              $"Message:\n{emailViewModel.MessageBody}\n\n" +
              $"Message Timestamp:\n{DateTime.UtcNow}\n\n" +
              $"Client Details:\n{await GetClientDetailsAsync()}";

            await _emailSender.SendAsync(messageBody, emailViewModel.MessageSubject);
            return RedirectToAction("EmailSent");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return View();
        }
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