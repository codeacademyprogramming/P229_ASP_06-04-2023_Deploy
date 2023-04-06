using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229MentorHomeWork.DataAccessLayer;
using P229MentorHomeWork.Models;
using P229MentorHomeWork.ViewModels.Pricing;

namespace P229MentorHomeWork.Controllers
{
    public class PricingController : Controller
    {
        private readonly AppDbContext _context;

        public PricingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Pricing> pricings = await _context.Pricings.Include(p=>p.PricingOffers).ToListAsync();
            IEnumerable<Offer> offers = await _context.Offers.ToListAsync();

            PricingVM pricingVM = new PricingVM
            {
                Pricings = pricings,
                Offers = offers
            };

            return View(pricingVM);
        }
    }
}
