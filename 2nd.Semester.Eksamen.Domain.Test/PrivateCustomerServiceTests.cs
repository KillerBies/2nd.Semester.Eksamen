using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Moq;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
namespace _2nd.Semester.Eksamen.Domain.Test;


[TestFixture]
public class PrivateCustomerServiceTests
{
    private Mock<IPrivateCustomerRepository> _mockRepository;
    private PrivateCustomerService _service;
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IPrivateCustomerRepository>();
        _service = new PrivateCustomerService(_mockRepository.Object);

    }

    [Test] //Tests Creating Private Customer from DTO to repository.
    public async Task CreatePrivateCustomerAsync_ReceivesCustomerAndRunsOnlyOnce()
    {
        //Arrange
        var dto = new PrivateCustomerDTO
        {
            FirstName = "Hans",
            LastName = "Hansen",
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
        _mockRepository.Verify(x => x.CreateNewAsync(It.IsAny<PrivateCustomer>()), Times.Once);
    }

    





}