using Microsoft.AspNetCore.Mvc;
using mini_store.Data;
using mini_store.Models;

namespace mini_store.Controllers;
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(AppDbContext cn, IWebHostEnvironment wsn)
    {
        _context = cn;
        _webHostEnvironment = wsn;
    }

    public IActionResult Index(string searchTerm)
    {
        var productsQuery = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            productsQuery= productsQuery.Where(p => p.Name.Contains(searchTerm));
        }

        ViewBag.CurrentSearch = searchTerm;
        var products = productsQuery.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View(products);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if(product.ImageFile != null)
        {
            var uploadFile = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }
            string extension = Path.GetExtension(product.ImageFile.FileName); //jpg
            string uniqueFileName = Guid.NewGuid().ToString() + extension; //48379879.jpg
            string filePath = Path.Combine(uploadFile , uniqueFileName);//wwwrooot/images/48379879.jpg
            product.Image = uniqueFileName;
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                product.ImageFile.CopyTo(fileStream);
            }
        }

        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View("Index" , _context.Products.ToList());
    }
    public IActionResult Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var product = _context.Products.FirstOrDefault(c => c.Id == id);
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}