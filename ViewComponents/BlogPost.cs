using Microsoft.AspNetCore.Mvc;

namespace Application.ViewComponents;

public class BlogPost : ViewComponent
{
    public IViewComponentResult Invoke(string fileRoute)
    {
        string markdownContent = System.IO.File.ReadAllText(fileRoute);

        string htmlContent = Markdig.Markdown.ToHtml(markdownContent);

        return View("Default", htmlContent);
    }
}