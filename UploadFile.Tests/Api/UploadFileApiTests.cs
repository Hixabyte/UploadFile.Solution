namespace UploadFile.Tests.Api
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using AutoMapper;
    using Moq;
    using Xunit;

    using UploadFile.Api.Controllers;
    using UploadFile.DataAccess.Repository;
    using UploadFile.Parser;

    public class UploadFileApiTests
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<IFormFile> _formFile;
        private readonly Mock<ICsvFileToEmployeeList> _parser;
        private readonly Mock<IMapper> _mapper;

        public UploadFileApiTests()
        {
            _formFile = new Mock<IFormFile>();
            _mockRepo = new Mock<IEmployeeRepository>();
            _parser = new Mock<ICsvFileToEmployeeList>();
            _mapper = new Mock<IMapper>();
            _controller = new EmployeeController(_mockRepo.Object, _parser.Object, _mapper.Object);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var content = @"Dr,Elton,Lyte,M,10/25/1968,elyte0@icq.com,Safety Technician III ,23945.00";
            var fileName = "data.txt";
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(content);
            streamWriter.Flush();
            memoryStream.Position = 0;
            _formFile.Setup(s => s.OpenReadStream()).Returns(memoryStream);
            _formFile.Setup(s => s.FileName).Returns(fileName);
            _formFile.Setup(s => s.Length).Returns(memoryStream.Length);
            var file = _formFile.Object;

            var urlHelperMock = new Mock<IUrlHelper>();

            urlHelperMock
                .Setup(m => m.Content("https://localhost:44375/api/employees"));

            // Act
            var postResult = await _controller.CreateEmployees(file);

            var foo = postResult as CreatedAtRouteResult;

            foo.UrlHelper = urlHelperMock.Object;

            // Assert
            Assert.IsType<CreatedAtRouteResult>(foo);
        }
    }
}