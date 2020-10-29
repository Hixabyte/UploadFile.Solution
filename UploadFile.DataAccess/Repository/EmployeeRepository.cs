namespace UploadFile.DataAccess.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UploadFile.Models;
    using UploadFile.DataAccess.Context;

    /// <summary>
    /// Accepts a DbContext and implements methods to Read and Create employee data.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly InterviewContext _context;

        public EmployeeRepository(InterviewContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates Employee records.
        /// </summary>
        /// <param name="employees">IEnumerable<EmployeeDto></param>
        /// <returns>Task</returns>
        public async Task CreateEmployees(IEnumerable<EmployeeDto> employees)
        {
            if (employees == null)
            {
                throw new ArgumentNullException(nameof(employees));
            }

            try
            {
                _context.Employee.AddRange(employees);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reads Employee records.
        /// </summary>
        /// <returns>IEnumerable<EmployeeDto></returns>
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _context.Employee;
        }
    }
}