using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductsCleanApi.Application.DTOs.EmployeeDto
{
    public class UpdateEmployeeDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, 10000000)]
        public decimal Salary { get; set; }
    }
}
