using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.Interfaces;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Moq;
namespace _2nd.Semester.Eksamen.Domain.Test;


[TestFixture]
public class CreateCustomerServiceTests
{
    private Mock<ICustomerRepository> _mockRepository;
    private CreateCustomerService _service;
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _service = new CreateCustomerService(_mockRepository.Object);

    }

    [Test] //Tests Creating Private Customer from DTO to repository.
    public async Task CreatePrivateCustomerAsync_ReceivesCustomerAndRunsOnlyOnce()
    {
        //Arrange
        var dto = new PrivateCustomerDTO
        {
            Name = "Hans",
            City = "TestCity",
            PostalCode = "1234",
            StreetName = "TestStreet",
            HouseNumber = "10",
            PhoneNumber = "87654321",
            Email = "test1@test1.com",
            Gender = Gender.Male,
            Birthday = new DateOnly(2000, 1, 1)
        };
        //Act
        await _service.CreatePrivateCustomerAsync(dto);
        //Assert
        _mockRepository.Verify(x => x.CreateNewCustomerAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Test] //Tests Creating Company Customer from DTO to repository.
    public async Task CreateCompanyCustomerAsync_ReceivesCustomerAndRunsOnlyOnce()
    {
        //arrange
        var dto = new CompanyCustomerDTO
        {
            Name = "TestCompany",
            City = "TestCity",
            PostalCode = "1234",
            StreetName = "TestStreet",
            HouseNumber = "10",
            PhoneNumber = "87654321",
            Email = "test1@test1.com",
            CVRNumber = "12345678"
        };
        //Act
        await _service.CreateCompanyCustomerAsync(dto);
        //Assert
        _mockRepository.Verify(x => x.CreateNewCustomerAsync(It.IsAny<Customer>()), Times.Once);
    }





}