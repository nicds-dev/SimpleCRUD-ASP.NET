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

    // Search by Name
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
        var employeeViewModel = new EmployeeViewModel
        {
            Employees = employees,
            SearchText = SearchText
        };

        return View(employeeViewModel);
    }

    // Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateCategories();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,CategoryId")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            try 
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Employee created successfully";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Something went wrong during employee creation.";
                TempData["MessageType"] = "danger";
            }
        }

        await PopulateCategories();
        return View(employee);
    }

    // Read (detail)
    [HttpGet]
    public async Task<IActionResult> Detail(int? id)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
        {
            TempData["Message"] = "The employee was not found.";
            TempData["MessageType"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        return View(employee);
    }

    // Update
    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
        {
            TempData["Message"] = "The employee was not found.";
            TempData["MessageType"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        await PopulateCategories();
        return View(employee);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,CategoryId")] Employee employee)
    {
        if (id != employee.Id)
        {
            TempData["Message"] = "The request is not valid.";
            TempData["MessageType"] = "danger";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Employee updated successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Something went wrong during employee update.";
                TempData["MessageType"] = "danger";
            }
        }

        await PopulateCategories();
        return View(employee);
    }

    // Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
        {
            TempData["Message"] = "The employee was not found.";
            TempData["MessageType"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Employee successfully deleted!";
            TempData["MessageType"] = "success";
        }
        catch
        {
            TempData["Message"] = "Something went wrong during employee delete.";
            TempData["MessageType"] = "danger";
        }

        return RedirectToAction(nameof(Index));
    }

    // Helper methods
    private async Task<Employee?> GetEmployeeById(int? id)
    {
        if (id == null) return null;

        return await _context.Employees
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    private async Task PopulateCategories()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
    }
}
