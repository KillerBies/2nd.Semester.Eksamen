using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using Moq;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
namespace _2nd.Semester.Eksamen.Domain.Test;


[TestFixture]
public class CompanyCustomerServiceTests
{
    private Mock<ICompanyCustomerRepository> _mockRepository;
    private CompanyCustomerService _service;
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<ICompanyCustomerRepository>();
        _service = new CompanyCustomerService(_mockRepository.Object);

    }

    [Test] //Tests Creating CompanyCustomer from DTO to repository.
    public async Task CreateCompanyCustomerAsync_ReceivesCustomerAndRunsOnlyOnce()
    {
        //Arrange
        var dto = new CompanyCustomerDTO
        {
            Name = "TestFirma",
            City = "Storby",
            PostalCode = "7100",
            StreetName = "Firmavej",
            HouseNumber = "32",
            PhoneNumber = "12345678",
            Email = "firma@testfirma.dk",
            CVRNumber ="87654321"


        };
        //Act
        await _service.CreateCompanyCustomerAsync(dto);
        //Assert
        _mockRepository.Verify(x => x.CreateNewAsync(It.IsAny<CompanyCustomer>()), Times.Once);
    }







}