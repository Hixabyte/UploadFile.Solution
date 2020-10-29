namespace UploadFile.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Employee Dto model
    /// </summary>
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        [Range(0, 999999999)]
        public decimal Salary { get; set; }
    }
}