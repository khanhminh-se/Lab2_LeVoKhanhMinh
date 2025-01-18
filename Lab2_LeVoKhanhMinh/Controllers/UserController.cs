using Lab2_LeVoKhanhMinh.Data;
using Lab2_LeVoKhanhMinh.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2_LeVoKhanhMinh.Controllers;

public class UserController : Controller
{
    // GET
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View("Index");
    }

   [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    
    [HttpPost]
    public IActionResult Edit( User user)
    {
        if (user.Id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}