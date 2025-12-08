using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;

public class CreateEmployeeCommand
{
    private readonly IEmployeeService _service;
    private readonly DTO_to_Domain _adapter;

    public CreateEmployeeCommand(IEmployeeService service, DTO_to_Domain adapter)
    {
        _service = service;
        _adapter = adapter;
    }

    public async Task ExecuteAsync(EmployeeInputDTO dto)
    {

        // map DTO -> Domain using adapter
        var employee = await _adapter.DTOEmployeeInputToDomain(dto);

        // delegate persistence to service
        await _service.CreateEmployeeAsync(employee);
    }
}
