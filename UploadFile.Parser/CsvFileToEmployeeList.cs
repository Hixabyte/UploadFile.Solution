namespace UploadFile.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using UploadFile.Models;

    /// <summary>
    /// CsvFileToEmployeeList class.
    /// Accepts an IFormFile or file path, opens a StreamReader, reads each StreamReader line,
    /// splits the line by delimiter and builds an employee object. 
    /// </summary>
    public class CsvFileToEmployeeList : ICsvFileToEmployeeList
    {
        /// <summary>
        /// Parse method accepts an IFormFile from the HTML UI.
        /// </summary>
        /// <param name="formFile">IFormFile</param>
        /// <returns>List<Employee></returns>
        public List<Employee> Parse(IFormFile formFile)
        {
            List<Employee> employees = new List<Employee>();

            string streamLine;

            using (var streamReader = new StreamReader(formFile.OpenReadStream()))
            {
                while (( streamLine = streamReader.ReadLine() ) != null)
                {
                    // Split the line by delimiter.
                    var employee = streamLine.Split(",").ToList();

                    // Build the object.
                    try
                    {
                        employees.Add(BuildObject(employee));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Parse method accepts a file path from the Console app.
        /// </summary>
        /// <param name="filePath">string</param>
        /// <returns>List<Employee></returns>
        public List<Employee> Parse(string filePath)
        {
            List<Employee> employees = new List<Employee>();

            string streamLine;

            using (var streamReader = new StreamReader(filePath))
            {
                if (streamReader != null)
                {
                    while (( streamLine = streamReader.ReadLine() ) != null)
                    {
                        // Split the line by delimiter.
                        var employee = streamLine.Split(",").ToList();

                        // Build the object.
                        try
                        {
                            employees.Add(BuildObject(employee));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// Build an Employee object from a List of strings.
        /// </summary>
        /// <param name="stringList">List<string></param>
        /// <returns>Employee</returns>
        public Employee BuildObject(List<string> stringList)
        {
            try
            {
                var newEmployee = new Employee
                {
                    Title = stringList[0].ToString().Trim(),
                    Forename = stringList[1].ToString().Trim(),
                    Surname = stringList[2].ToString().Trim(),
                    Gender = stringList[3].ToString().Trim(),
                    BirthDate = DateTime.ParseExact(stringList[4].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Email = stringList[5].ToString().Trim(),
                    Role = stringList[6].ToString().Trim(),
                    Salary = Decimal.Parse(stringList[7].Trim())
                };

                return newEmployee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
