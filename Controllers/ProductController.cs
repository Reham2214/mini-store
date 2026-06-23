using Microsoft.AspNetCore.Mvc;
using mini_store.Data;
using mini_store.Models;

namespace mini_store.Controllers;
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext cn)
    {
        _context = cn;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View("Create" , products);
    }
    public IActionResult Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
         return RedirectToAction("Index");
    }
}