using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mini_store.Models;

namespace mini_store.Controllers;

public class HomeController : Controller
{
    private static dynamic[] _categories =
    {
        new { Id = 0, Name = "إلكترونيات", Icon = "fa-solid fa-bolt-lightning" },
        new { Id = 1, Name = "ملابس", Icon = "fa-solid fa-shirt" },
        new { Id = 2, Name = "كتب", Icon = "fas fa-book-open" }
    };
    private static dynamic[] _products =
    {
        new { Id = 0, CategoryId = 0, Name = "هاتف ذكي", Price = 2500, Description = "هاتف ذكي بكاميرا عالية الدقة", Image = "phone.webp" },
        new { Id = 1, CategoryId = 0, Name = "حاسوب محمول", Price = 4500, Description = "حاسوب مخصص للمطورين", Image = "laptop.png" },
        new { Id = 2, CategoryId = 1, Name = "قميص قطني", Price = 150, Description = "قميص مريح وصيفي", Image = "shirt.jpg" },
        new { Id = 3, CategoryId = 2, Name = "كتاب برمجة", Price = 90, Description = "دليل شامل لتعلم البرمجة", Image = "book.webp" }
    };
    public IActionResult Index()
    {
        ViewBag.CategoriesList = _categories;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Products(int id)
    {
        var filterdList = _products.Where(p => p.CategoryId == id).ToList();
        
        ViewBag.FilterdProducts = filterdList;
        return View();
    }

    public IActionResult Details (int id)
    {
        var Product = _products.FirstOrDefault(p => p.Id == id);

        if(Product == null)
        {
            return NotFound();
        }

        ViewBag.Product = Product;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
