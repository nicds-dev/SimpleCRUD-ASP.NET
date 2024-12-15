using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.ViewModels;

public class EmployeeViewModel
{
    public List<Models.Employee>? Employees { get; set;}
    public string? SearchText { get; set; }
}
