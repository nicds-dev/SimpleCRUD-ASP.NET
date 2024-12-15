using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Data;
using SimpleCRUD.Models;
using SimpleCRUD.ViewModels;

namespace SimpleCRUD.Controllers;

public class EmployeeController : Controller
{
    private readonly DataContext _context;

    public EmployeeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index(string SearchText)
    {
        var employees = _context.Employees
            .Include(e => e.Category)
            .ToList();
        if (!string.IsNullOrEmpty(SearchText))
        {
            employees = employees
                .Where(e => e.FirstName.ToLower().Contains(SearchText.ToLower()) ||
                            e.LastName.ToLower().Contains(SearchText.ToLower()))
            .ToList();
        }
        var employeeViewModel = new EmployeeViewModel();
        employeeViewModel.Employees = employees;
        employeeViewModel.SearchText = SearchText;

        return View(employeeViewModel);
    }

    // Create
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

    // Read
    [HttpGet]
    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // Update
    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View(employee);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,CategoryId")] Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View(employee);
    }

    // Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
