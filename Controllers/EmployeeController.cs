using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Data;

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
        var employees = _context.Employees.ToList();
        return View(employees);
    }
}
