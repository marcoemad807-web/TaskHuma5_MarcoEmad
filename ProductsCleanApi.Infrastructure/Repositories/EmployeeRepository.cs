using Microsoft.EntityFrameworkCore;
using ProductsCleanApi.Application.DTOs.EmployeeDto;
using ProductsCleanApi.Application.Interfaces;
using ProductsCleanApi.Domain.Entities;
using ProductsCleanApi.Infrastructure.Persistence;

namespace ProductsCleanApi.Infrastructure.Repositories
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Email = e.Email,
                    Salary = e.Salary
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<EmployeeDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Email = e.Email,
                    Salary = e.Salary
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<EmployeeDto> CreateAsync(
            CreateEmployeeDto dto,
            CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Salary = dto.Salary
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                Salary = employee.Salary
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateEmployeeDto dto,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee is null)
                return false;

            employee.FullName = dto.FullName;
            employee.Email = dto.Email;
            employee.Salary = dto.Salary;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee is null)
                return false;

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
