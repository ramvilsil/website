using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels;

namespace Application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

    [Route("/skills")]
    public IActionResult Skills() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}