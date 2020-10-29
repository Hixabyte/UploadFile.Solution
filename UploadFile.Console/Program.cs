namespace UploadFile.Console
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using UploadFile.Models;
    using UploadFile.Parser;
    using UploadFile.Console.Profiles;
    using UploadFile.DataAccess.Context;
    using UploadFile.DataAccess.Repository;

    /// <summary>
    /// Requests a file path to the data file, parses the data file,
    /// maps the data to a Dto and adds to the repository.
    /// </summary>
    class Program
    {
        static async Task Main()
        {
            var mapperConfig = new MapperConfiguration(options =>
            {
                options.AddProfile<EmployeesProfile>();
            });

            var mapper = new Mapper(mapperConfig);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("UploadFileConsole: Read, Parse and Store the data.txt file");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"Please enter the data.txt file's location (e.g. C:\)");

            var rootFolder = Console.ReadLine();

            if (string.IsNullOrEmpty(rootFolder))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter a file location.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var filePath = rootFolder + "data.txt";

            List<Employee> employees = new List<Employee>();

            var parser = new CsvFileToEmployeeList();

            try
            {
                employees = parser.Parse(filePath);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File is empty.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("File content parsed.");
            Console.ForegroundColor = ConsoleColor.White;

            var context = new InterviewContext();

            var repository = new EmployeeRepository(context);

            // Map the data to a Dto and add to repository.
            try
            {
                var employeeModel = mapper.Map<List<EmployeeDto>>(employees);
                await repository.CreateEmployees(employeeModel);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("File contents uploaded successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
