using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using Moq;
namespace _2nd.Semester.Eksamen.Domain.Test;


[TestFixture]
public class EmployeeServiceTests
{
    private Mock<IEmployeeRepository> _mockEmployeeRepository;
    private Mock<IAddressRepository> _mockAddressRepository;
    private EmployeeService _service;
    [SetUp]
    public void Setup()
    {
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _mockAddressRepository = new Mock<IAddressRepository>();
        _service = new EmployeeService(_mockEmployeeRepository.Object, _mockAddressRepository.Object);

    }

    [Test] //Tests Creating CompanyCustomer from DTO to repository.
    public async Task CreateEmployeeAsync_ReceivesEmployeeAndRunsOnlyOnce()
    {
        //Arrange
        var dto = new EmployeeInputDTO
        {

        };
        //Act
        await _service.
        //Assert
        _mockEmployeeRepository.Verify(x => x.CreateNewAsync(It.IsAny<CompanyCustomer>()), Times.Once);
    }







}