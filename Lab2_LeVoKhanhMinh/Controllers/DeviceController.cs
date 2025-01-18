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
        ViewBag.categories = _context.DeviceCategories.ToList();
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
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

    [HttpPost]
    public IActionResult Delete(int Id)
    {
        var device = _context.Devices.Find(Id);
        _context.Devices.Remove(device);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int deviceId)
    {   
        var device = _context.Devices.Find(deviceId);
        ViewBag.categories = _context.DeviceCategories.ToList();
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        ViewBag.device = device;
        return View();
    }

    [HttpPost]
    public IActionResult Edit(Device device)
    {
        var previousDevice = _context.Devices.Find(device.Id);
        if (previousDevice == null)
        {
            return NotFound();
        }
        else
        {
            previousDevice.DeviceCode = device.DeviceCode;
            previousDevice.DeviceName = device.DeviceName;
            previousDevice.Status = device.Status;
            previousDevice.DateOfEntry = device.DateOfEntry;
            previousDevice.DeviceCategoryId = device.DeviceCategoryId;
            previousDevice.DeviceCategory = device.DeviceCategory;
            _context.Devices.Update(previousDevice);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AllStatuses()
    {
        ViewBag.filterType = "Status";
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        var devices = _context.Devices.Include(d => d.DeviceCategory).ToList();
        ViewBag.devices = devices;
        return View("Index");
    }
    [HttpGet]
    public IActionResult AllCategories()
    {
        ViewBag.filterType = "Category";
        ViewBag.categories = _context.DeviceCategories.ToList();
        var devices = _context.Devices.Include(d => d.DeviceCategory).ToList();
        ViewBag.devices = devices;
        return View("Index");
    }

    [HttpGet]
    public IActionResult Search(string searchTerm)
    {
        var devices = _context.Devices.Include(d => d.DeviceCategory).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            devices = devices.Where(d => d.DeviceName.Contains(searchTerm) || d.DeviceCode.Contains(searchTerm));
        }

        ViewBag.categories = _context.DeviceCategories.ToList();
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        ViewBag.devices = devices.ToList();

        return View("Index");  
    }
    [HttpGet]
    public IActionResult FilterDevices(string filterBy, string filterValue)
    {
        var devices = _context.Devices.Include(d => d.DeviceCategory).AsQueryable();

        if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
        {
            if (filterBy == "Category")
            {
                var categoryId = Convert.ToInt32(filterValue);
                devices = devices.Where(d => d.DeviceCategoryId == categoryId);
            }
            else if (filterBy == "Status")
            {
                devices = devices.Where(d => d.Status.ToString() == filterValue);
            }
        }

        ViewBag.devices= devices.ToList();
        ViewBag.categories = _context.DeviceCategories.ToList();
        ViewBag.Statuses = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        return PartialView("Index"); 
    }

  
}