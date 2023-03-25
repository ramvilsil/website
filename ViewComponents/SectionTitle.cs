using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.ViewComponents
{
    public class SectionTitle : ViewComponent
    {
        public IViewComponentResult Invoke(string title) => View("Default", title);
    }
}