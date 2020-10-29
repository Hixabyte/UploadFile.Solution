namespace UploadFile.Parser
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using UploadFile.Models;

    /// <summary>
    /// ICsvFileToEmployeeList interface.
    /// Specifies method signatures to be implemented by concrete classes.
    /// </summary>
    public interface ICsvFileToEmployeeList
    {
        List<Employee> Parse(string filePath);
        List<Employee> Parse(IFormFile formFile);
        Employee BuildObject(List<string> stringList);
    }
}
