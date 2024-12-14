using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(15, ErrorMessage = "Name cannot exceed 15 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(150, ErrorMessage = "First Name cannot exceed 150 characters.")]
    public required string Description { get; set; }

    public List<Employee>? Employees { get; set; }
}
