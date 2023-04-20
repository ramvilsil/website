using Microsoft.AspNetCore.Mvc;

namespace Application.ViewComponents
{
    public class ProjectDisplay : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string? imageSrc, string? textArea, string? liveLink, string? sourceLink)
        {
            return View("Default", new
            {
                title,
                imageSrc,
                textArea,
                liveLink,
                sourceLink
            });
        }
    }
}