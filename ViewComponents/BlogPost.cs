using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.ViewComponents
{
    public class BlogPost : ViewComponent
    {
        public IViewComponentResult Invoke(string topic, string title, string? imageSrc, string? textArea1, string? textArea2, string? code, string? codeLanguage)
        {
            return View("Default", new
            {
                topic,
                title,
                imageSrc,
                textArea1,
                textArea2,
                code,
                codeLanguage
            });
        }
    }
}