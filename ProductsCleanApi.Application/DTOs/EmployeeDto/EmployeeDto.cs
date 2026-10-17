using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsCleanApi.Application.DTOs.EmployeeDto
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal Salary { get; set; }
    }
}
