using Lab2_LeVoKhanhMinh.Data;
using Lab2_LeVoKhanhMinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2_LeVoKhanhMinh.Controllers;

public class DeviceController : Controller
{   
    private readonly ApplicationDbContext _context;

    public DeviceController(ApplicationDbContext context)
    {
     _context = context;    
    }
    // GET
    public IActionResult Index()
    {
        var devices = _context.Devices.Include(d => d.DeviceCategory).ToList();
        ViewBag.devices = devices;
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {  
        ViewBag.categories = _context.DeviceCategories.ToList();
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Device device)
    {
        if (device == null)
        {
            Console.WriteLine("Device is null");
            return BadRequest();
        }
        var category = _context.DeviceCategories.Find(device.DeviceCategoryId);
        if (category == null)
        {
            return NotFound();
        }
        device.DeviceCategory = category;
        _context.Devices.Add(device);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}