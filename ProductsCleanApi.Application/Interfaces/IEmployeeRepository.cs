using ProductsCleanApi.Application.DTOs.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsCleanApi.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<EmployeeDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);

        Task<EmployeeDto> CreateAsync(
            CreateEmployeeDto dto,
            CancellationToken cancellationToken);

        Task<bool> UpdateAsync(
            int id,
            UpdateEmployeeDto dto,
            CancellationToken cancellationToken);

        Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken);
    }
}
