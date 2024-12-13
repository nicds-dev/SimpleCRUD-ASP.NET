using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Data;
using SimpleCRUD.Models;

namespace SimpleCRUD.Controllers;

public class EmployeeController : Controller
{
    private readonly DataContext _context;

    public EmployeeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var employees = _context.Employees
            .Include(c => c.Category)
            .ToList();

        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,CategoryId")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View();
    }
}
