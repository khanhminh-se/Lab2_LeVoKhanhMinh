using Lab2_LeVoKhanhMinh.Data;
using Lab2_LeVoKhanhMinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2_LeVoKhanhMinh.Controllers;

public class DeviceCategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public DeviceCategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        var deviceCategories = _context.DeviceCategories.Include(x => x.Devices).ToList();
        return View(deviceCategories); 
    }
        
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Title = "Create New Category";
        return View();
    }

    [HttpPost]
    public IActionResult Create(string CategoryTitle, IFormFile ImageFile)
    {
        if (ModelState.IsValid)
        {
            var maxId = _context.DeviceCategories.Max(x =>(int?) x.Id) ?? 0;
            var nextId = maxId + 1;
            var category = new DeviceCategory
            {
                Id = nextId,
               CategoryTitle = CategoryTitle,
                imageUrl = "images/default-image.png",
            };
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                category.imageUrl = "images/" + fileName;
            }
            _context.DeviceCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var deviceCategory  = _context.DeviceCategories.Find(id);
        if (deviceCategory == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(deviceCategory.imageUrl))
        {
            var filePath = Path.Combine("wwwroot", deviceCategory.imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        _context.DeviceCategories.Remove(deviceCategory);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int Id)
    
    { 
        
        var deviceCategory = _context.DeviceCategories.Find(Id);
        if (deviceCategory == null)
        {
            Console.WriteLine("Device category not found");
            return NotFound();
        }
        return View(deviceCategory);
    }

    [HttpPost]
    public IActionResult Edit(int Id, string CategoryTitle, IFormFile ImageFile)
    {
       var category  = _context.DeviceCategories.Find(Id);
       if (category == null)
       {
           return NotFound();
       }
       else
       {
           if (ImageFile != null && ImageFile.Length > 0)
           {
               var fileName = Path.GetFileName(ImageFile.FileName);
               var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
               using (var stream = new FileStream(filePath, FileMode.Create))
               {
                   ImageFile.CopyTo(stream);
               }

               category.imageUrl = "images/" + fileName;
           }

           category.CategoryTitle = CategoryTitle;
           _context.Update(category);
           _context.SaveChanges();
       }
        return RedirectToAction(nameof(Index));
    }
   
}