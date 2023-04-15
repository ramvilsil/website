using Microsoft.AspNetCore.Mvc;

namespace Application.ViewComponents;

public class SectionTitle : ViewComponent
{
    public IViewComponentResult Invoke(string title) => View("Default", title);
}