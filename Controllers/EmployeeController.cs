using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Models;

namespace SimpleCRUD.Controllers;

public class EmployeeController : Controller
{
    private static List<Employee> employees =
    [
        new Employee { Id = 1, FirstName = "Pepito", LastName = "Roso", Email = "em@il.com", PhoneNumber = "311223344", EmploymentType = "Full Time" },
        new Employee { Id = 2, FirstName = "Juan", LastName = "Perez", Email = "juan@ejemplo.com", PhoneNumber = "322334455", EmploymentType = "Part Time" }
    ];
    
    public IActionResult Index()
    {
        return View(employees);
    }
}
