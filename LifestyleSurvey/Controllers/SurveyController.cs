using LifestyleSurvey.Data;
using LifestyleSurvey.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LifestyleSurvey.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyContext _context;

        public SurveyController(SurveyContext context)
        {
            _context = context;
        }

        // GET: Survey
        public IActionResult Index()
        {
            return View();
        }

        // POST: Survey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullNames,Email,DateOfBirth,ContactNumber,FavoriteFoods,MoviesRating,RadioRating,EatOutRating,TVRating")]
 SurveyResponse response)
        {
            if (ModelState.IsValid)
            {
                // Validate age (between 5 and 120)
                var age = DateTime.Today.Year - response.DateOfBirth.Year;
                if (response.DateOfBirth > DateTime.Today.AddYears(-age)) age--;

                if (age < 5 || age > 120)
                {
                    ModelState.AddModelError("DateOfBirth", "Age must be between 5 and 120 years.");
                    return View("Index", response);
                }

                response.SubmissionDate = DateTime.Now;
                _context.SurveyResponses.Add(response);
                await _context.SaveChangesAsync();

                return RedirectToAction("ThankYou");
            }

            return View("Index", response);
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        public async Task<IActionResult> Results()
        {
            var responses = await _context.SurveyResponses.ToListAsync();

            if (!responses.Any())
            {
                ViewBag.Message = "No Surveys Available";
                return View();
            }

            // Calculate statistics
            var totalSurveys = responses.Count;

            // Age calculations
            var today = DateTime.Today;
            var ages = responses.Select(r => today.Year - r.DateOfBirth.Year -
                                        (r.DateOfBirth > today.AddYears(-(today.Year - r.DateOfBirth.Year)) ? 1 : 0)).ToList();
            var averageAge = Math.Round(ages.Average(), 1);
            var oldest = ages.Max();
            var youngest = ages.Min();

            // Food preferences
            var pizzaCount = responses.Count(r => r.FavoriteFoods.Contains("Pizza"));
            var pastaCount = responses.Count(r => r.FavoriteFoods.Contains("Pasta"));
            var papWorsCount = responses.Count(r => r.FavoriteFoods.Contains("Pap and Wors"));

            var pizzaPercentage = Math.Round((double)pizzaCount / totalSurveys * 100, 1);
            var pastaPercentage = Math.Round((double)pastaCount / totalSurveys * 100, 1);
            var papWorsPercentage = Math.Round((double)papWorsCount / totalSurveys * 100, 1);

            // Average ratings
            var moviesAvg = Math.Round(responses.Average(r => r.MoviesRating), 1);
            var radioAvg = Math.Round(responses.Average(r => r.RadioRating), 1);
            var eatOutAvg = Math.Round(responses.Average(r => r.EatOutRating), 1);
            var tvAvg = Math.Round(responses.Average(r => r.TVRating), 1);

            ViewBag.TotalSurveys = totalSurveys;
            ViewBag.AverageAge = averageAge;
            ViewBag.OldestAge = oldest;
            ViewBag.YoungestAge = youngest;
            ViewBag.PizzaPercentage = pizzaPercentage;
            ViewBag.PastaPercentage = pastaPercentage;
            ViewBag.PapWorsPercentage = papWorsPercentage;
            ViewBag.MoviesRating = moviesAvg;
            ViewBag.RadioRating = radioAvg;
            ViewBag.EatOutRating = eatOutAvg;
            ViewBag.TVRating = tvAvg;

            return View();
        }
    }
}