using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Test.ApplicationTests.PersonTests.EmployeeTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private Mock<IAddressRepository> _addressRepoMock;
        private EmployeeService _employeeService;
        private DTO_to_Domain _dtoToDomain;

        [SetUp]
        public void Setup()
        {
            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _addressRepoMock = new Mock<IAddressRepository>();

            _employeeService = new EmployeeService(_employeeRepoMock.Object, _addressRepoMock.Object);

            // We can pass mocks as null if they aren’t used in this helper
            _dtoToDomain = new DTO_to_Domain(null, null, _employeeRepoMock.Object, null);
        }

        [Test]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployeeAndAddress()
        {
            // Arrange
            var employeeId = 1;
            var existingEmployee = new Employee(
                firstname: "OldFirst",
                lastname: "OldLast",
                email: "old@email.com",
                phoneNumber: "12345678",
                address: new Address("OldCity", "0000", "OldStreet", "1"),
                basePriceMultiplier: 1.0m,
                experience: "Intermediate",
                type: "Staff",
                specialties: "Specialty1",
                gender: "Male",
                workStart: new TimeOnly(8, 0, 0),
                workEnd: new TimeOnly(18, 0, 0)
            )
            {
                Id = employeeId
            };

            _employeeRepoMock.Setup(x => x.GetByIDAsync(employeeId))
                             .ReturnsAsync(existingEmployee);

            var updateDto = new EmployeeUpdateDTO
            {
                FirstName = "NewFirst",
                LastName = "NewLast",
                Email = "new@email.com",
                PhoneNumber = "87654321",
                Specialties = new List<SpecialtyItemBase> { new SpecialtyItemBase { Value = "NewSpecialty" } },
                Gender = Gender.Male,
                BasePriceMultiplier = 1.5m,
                ExperienceLevel = ExperienceLevels.Senior,
                Type = EmployeeType.Staff,
                Address = new AddressUpdateDTO
                {
                    City = "NewCity",
                    PostalCode = "1111",
                    StreetName = "NewStreet",
                    HouseNumber = "42"
                }
            };

            // Act
            var updatedEmployee = await _dtoToDomain.DTOEmployeeUpdateToDomain(employeeId, updateDto);

            // Assert
            Assert.That(updatedEmployee.Name, Is.EqualTo("NewFirst"));
            Assert.That(updatedEmployee.LastName, Is.EqualTo("NewLast"));
            Assert.That(updatedEmployee.Email, Is.EqualTo("new@email.com"));
            Assert.That(updatedEmployee.PhoneNumber, Is.EqualTo("87654321"));
            Assert.That(updatedEmployee.Specialties, Is.EqualTo("NewSpecialty"));
            Assert.That(updatedEmployee.BasePriceMultiplier, Is.EqualTo(1.5m));
            Assert.That(updatedEmployee.ExperienceLevel, Is.EqualTo("Senior"));
            Assert.That(updatedEmployee.Type, Is.EqualTo("Staff"));
            Assert.That(updatedEmployee.Gender, Is.EqualTo("Male"));

            Assert.That(updatedEmployee.Address.City, Is.EqualTo("NewCity"));
            Assert.That(updatedEmployee.Address.PostalCode, Is.EqualTo("1111"));
            Assert.That(updatedEmployee.Address.StreetName, Is.EqualTo("NewStreet"));
            Assert.That(updatedEmployee.Address.HouseNumber, Is.EqualTo("42"));

            _employeeRepoMock.Verify(x => x.UpdateAsync(existingEmployee), Times.Never); // Service hasn’t called UpdateAsync yet
        }
        [Test]
        public async Task CreateEmployeeAsync_ShouldCreateEmployeeCorrectly()
        {
            // Arrange
            var createDto = new EmployeeInputDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "John@Doe.com",
                PhoneNumber = "12345678",
                Specialties = new List<SpecialtyItemBase> { new SpecialtyItemBase { Value = "Massage" } },
                Gender = Gender.Male,
                BasePriceMultiplier = 1.2m,
                ExperienceLevel = ExperienceLevels.Senior,
                Type = EmployeeType.Staff,
                Address = new AddressInputDTO
                {
                    City = "Test City",
                    PostalCode = "1234",
                    StreetName = "Test Street",
                    HouseNumber = "10"
                }
            };

            Employee capturedEmployee = null;
            _employeeRepoMock.Setup(x => x.CreateNewAsync(It.IsAny<Employee>()))
                             .Callback<Employee>(emp => capturedEmployee = emp)
                             .Returns(Task.CompletedTask);

            // Act
            var CreatedEmployee = await _dtoToDomain.DTOEmployeeInputToDomain(createDto);
            await _employeeRepoMock.Object.CreateNewAsync(CreatedEmployee);

            // Assert
            Assert.That(capturedEmployee.Name, Is.EqualTo("John"));
            Assert.That(capturedEmployee.LastName, Is.EqualTo("Doe"));
            Assert.That(capturedEmployee.Email, Is.EqualTo("John@Doe.com"));
            Assert.That(capturedEmployee.PhoneNumber, Is.EqualTo("12345678"));
            Assert.That(capturedEmployee.Specialties, Is.EqualTo("Massage"));
            Assert.That(capturedEmployee.BasePriceMultiplier, Is.EqualTo(1.2m));
            Assert.That(capturedEmployee.ExperienceLevel, Is.EqualTo("Senior"));
            Assert.That(capturedEmployee.Type, Is.EqualTo("Staff"));
            Assert.That(capturedEmployee.Gender, Is.EqualTo("Male"));

            Assert.That(capturedEmployee.Address.City, Is.EqualTo("Test City"));
            Assert.That(capturedEmployee.Address.PostalCode, Is.EqualTo("1234"));
            Assert.That(capturedEmployee.Address.StreetName, Is.EqualTo("Test Street"));
            Assert.That(capturedEmployee.Address.HouseNumber, Is.EqualTo("10"));

            _employeeRepoMock.Verify(x => x.CreateNewAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task ReadEmployeeDetailsAsync_ShouldReadEmployeeCorrectly()
        {
            // Arrange
            var employeeId = 1;
            var existingEmployee = new Employee(
                firstname: "Jane",
                lastname: "Doe",
                email: "jane@doe.com",
                phoneNumber: "87654321",
                address: new Address("Test City", "5678", "Test Street", "20"),
                basePriceMultiplier: 1.3m,
                experience: "Intermediate",
                type: "Staff",
                specialties: "Haircut",
                gender: "Female",
                workStart: new TimeOnly(8, 0, 0),
                workEnd: new TimeOnly(17, 0, 0)
            )
            {
                Id = employeeId
            };

            _employeeRepoMock.Setup(x => x.GetByIDAsync(employeeId))
                             .ReturnsAsync(existingEmployee);

            var readCommand = new ReadEmployeeDetailsCommand(_employeeService);

            // Act
            var result = await readCommand.ExecuteAsync(employeeId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(employeeId));
            Assert.That(result.FirstName, Is.EqualTo("Jane"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
            Assert.That(result.Email, Is.EqualTo("jane@doe.com"));
            Assert.That(result.PhoneNumber, Is.EqualTo("87654321"));
            Assert.That(result.Specialty, Is.EqualTo("Haircut"));
            Assert.That(result.BasePriceMultiplier, Is.EqualTo(1.3m));
            Assert.That(result.Experience, Is.EqualTo("Intermediate"));
            Assert.That(result.Type, Is.EqualTo("Staff"));
            Assert.That(result.Gender, Is.EqualTo("Female"));

            Assert.That(result.City, Is.EqualTo("Test City"));
            Assert.That(result.PostalCode, Is.EqualTo("5678"));
            Assert.That(result.StreetName, Is.EqualTo("Test Street"));
            Assert.That(result.HouseNumber, Is.EqualTo("20"));

            _employeeRepoMock.Verify(x => x.GetByIDAsync(employeeId), Times.Once);
        }
        [Test]
        public async Task ReadEmployeeUserCardsAsync_ShouldReturnAllEmployeeUserCards()
        {
            // Arrange
            var employees = new List<Employee>
    {
        new Employee(
            firstname: "Jane",
            lastname: "Doe",
            email: "Jane.Doe@company.com",
            phoneNumber: "11122233",
            address: new Address("Vejle", "7100", "Kolding Vej", "1"),
            basePriceMultiplier: 1.1m,
            experience: "Junior",
            type: "Staff",
            specialties: "Haircut",
            gender: "Female",
            workStart: new TimeOnly(8, 0, 0),
            workEnd: new TimeOnly(17, 0, 0))
        { Id = 1 },

        new Employee(
            firstname: "John",
            lastname: "Doe",
            email: "John.Doe@company.com",
            phoneNumber: "44455566",
            address: new Address("Kolding", "6000", "Vejle vej", "2"),
            basePriceMultiplier: 1.2m,
            experience: "Senior",
            type: "Staff",
            specialties: "Massage",
            gender: "Male",
            workStart: new TimeOnly(9, 0, 0),
            workEnd: new TimeOnly(18, 0, 0))
        { Id = 2 }
    };

            _employeeRepoMock.Setup(x => x.GetAllAsync())
                             .ReturnsAsync(employees);

            var readCommand = new ReadEmployeeUserCardsCommand(_employeeService);

            // Act
            var result = await readCommand.ExecuteAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstCard = result.First(e => e.Id == 1);
            Assert.That(firstCard.Name, Is.EqualTo("Jane"));
            Assert.That(firstCard.Type, Is.EqualTo("Staff"));
            Assert.That(firstCard.PhoneNumber, Is.EqualTo("11122233"));

            var secondCard = result.First(e => e.Id == 2);
            Assert.That(secondCard.Name, Is.EqualTo("John"));
            Assert.That(secondCard.Type, Is.EqualTo("Staff"));
            Assert.That(secondCard.PhoneNumber, Is.EqualTo("44455566"));

            _employeeRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteEmployeeAsync_ShouldRemoveEmployeeAndAddress()
        {
            // Arrange
            var employeeId = 1;
            var address = new Address("City", "1234", "Street", "10");
            var employee = new Employee(
                firstname: "John",
                lastname: "Doe",
                email: "john@doe.com",
                phoneNumber: "12345678",
                address: address,
                basePriceMultiplier: 1.2m,
                experience: "Senior",
                type: "Staff",
                specialties: "Massage",
                gender: "Male",
                workStart: new TimeOnly(8, 0, 0),
                workEnd: new TimeOnly(17, 0, 0))
            { Id = employeeId };

            _employeeRepoMock.Setup(x => x.GetByIDAsync(employeeId)).ReturnsAsync(employee);
            _employeeRepoMock.Setup(x => x.DeleteAsync(employee)).Returns(Task.CompletedTask);
            _addressRepoMock.Setup(x => x.DeleteAsync(address)).Returns(Task.CompletedTask);

            // Act
            await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            _employeeRepoMock.Verify(x => x.DeleteAsync(employee), Times.Once);
            _addressRepoMock.Verify(x => x.DeleteAsync(address), Times.Once);
        }


    }
}
