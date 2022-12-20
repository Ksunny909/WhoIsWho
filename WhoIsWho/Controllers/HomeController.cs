using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhoIsWho.Models;
using WhoIsWho.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WhoIsWho.Models.Entities;
using AutoMapper;

namespace WhoIsWho.Controllers
{
   // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = context;
        }

        [AllowAnonymous]
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Portfolio2()
        {
            return View("Portfolio2");
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] PostBookingModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errors"] = new List<string>() { "errorrr"};
                return RedirectToAction(nameof(Index));
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == User.Id());

            if (user == null)
            {
                return NotFound();
            }

            var booking = _mapper.Map<Booking>(model);
            booking.User = user;
            _dbContext.Bookings.Add(booking);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Portfolio1")]
        public IActionResult Portfolio1()
        {
            return View();
        }
       
        [AllowAnonymous]
        [HttpGet("Portfolio3")]
        public IActionResult Portfolio3()
        {
            return View();
        }
        [AllowAnonymous, HttpGet("Calendar/Info")]
        public async Task<IActionResult> Info(DateTime start, DateTime end)
        {
            var events = await _dbContext.Bookings.Include(b => b.User).AsNoTracking().ToListAsync();

            return Ok(events.Select(e => new { Start = e.Date, End = e.Date.AddHours(1), Title = $"Запись для {e.User.Email}" }));
        }

    }
}