namespace UploadFile.DataAccess.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UploadFile.Models;

    /// <summary>
    /// IEmployeeRepository interface.
    /// Specifies method signatures to be implemented by concrete classes.
    /// </summary>
    public interface IEmployeeRepository
    {
        public Task CreateEmployees(IEnumerable<EmployeeDto> employees);
        public IEnumerable<EmployeeDto> GetAllEmployees();
    }
}
