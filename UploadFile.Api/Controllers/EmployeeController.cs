namespace UploadFile.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;

    using UploadFile.Models;
    using UploadFile.Parser;
    using UploadFile.DataAccess.Repository;

    /// <summary>
    /// Employee Controller
    /// </summary>
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ICsvFileToEmployeeList _parser;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository repository,
            ICsvFileToEmployeeList parser,
            IMapper mapper)
        {
            _repository = repository;
            _parser = parser;
            _mapper = mapper;
        }

        /// <summary>
        /// Create Employees Controller Action.
        /// Creates database records from a comma separated file.
        /// </summary>
        /// <param name="formFile">IFormFile</param>
        /// <returns>Task<ActionResult></returns>
        //POST api/employee
        [HttpPost]
        public async Task<ActionResult> CreateEmployees([FromForm] IFormFile formFile)
        {
            List<Employee> employees;

            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("Unable to upload the file. The file was null or empty");
            }

            // Parse the file.
            try
            {
                employees = _parser.Parse(formFile);
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to parse the CSV file. Please check the format of the file contents. (" + ex.Message + ")");
            }

            // Map the data to a Dto and add to repository.
            try
            {
                var employeeModel = _mapper.Map<List<EmployeeDto>>(employees);
                await _repository.CreateEmployees(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Could not upload data to the repository: (" + ex.Message + ")");
            }

            //return CreatedAtRoute(Url.ActionLink("GetAllEmployees"), "File uploaded successfully.");

            return CreatedAtRoute("GetAllEmployees", employees);
        }

        /// <summary>
        /// Create Employees Controller Action.
        /// Returns all Employees from the repository.
        /// </summary>
        /// <returns>ActionResult</returns>
        //// GET api/employee
        [HttpGet(Name = "GetAllEmployees")]
        public ActionResult GetAllEmployees()
        {
            try
            {
                return Ok(_repository.GetAllEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Could not return data from the repository: (" + ex.Message + ")");
            }
        }
    }
}

