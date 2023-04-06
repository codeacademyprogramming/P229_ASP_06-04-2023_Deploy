using Microsoft.AspNetCore.Mvc;

namespace P229Allup.ViewComponents
{
    public class NewsLetterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
