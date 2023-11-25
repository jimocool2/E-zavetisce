using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_zavetisce.Models;
using E_zavetisce.Data;

namespace E_zavetisce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ZavetisceContext _context;

    public HomeController(ILogger<HomeController> logger, ZavetisceContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var notifications = _context.Notifications
            .OrderByDescending(n => n.DateCreated)
            .Take(3)
            .ToList();
        ViewData["Notifications"] = notifications;

        var pets = _context.Pets.
            OrderByDescending(n => n.DateAdded).
                Take(3).
                ToList();

        ViewData["Pets"] = pets;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
