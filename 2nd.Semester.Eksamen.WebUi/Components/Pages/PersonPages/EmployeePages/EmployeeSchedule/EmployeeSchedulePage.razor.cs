using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Schedule.Internal;

namespace Components.Pages.PersonPages.EmployeePages.EmployeeSchedule
{
    partial class EmployeeSchedulePage
    {
        [Inject] IScheduleService _scheduleService { get; set; }
        private List<EmployeeDTO> Employees { get; set; }
        private DateTime CurrentDate => @DateTime.Now;
        private List<ScheduleItem> ScheduleItems { get; set; }
        private int SelectedEmployeeId { get; set; }
        private string _errorMessage { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }
        private async Task GetEmployeeSchedule(int EmployeeId)
        {
            int i = 0;
            var Schedule = await _scheduleService.GetEmployeeSchedule(EmployeeId);
            ScheduleItems = Schedule.SelectMany(s => s.TimeRanges.Select(tr => new ScheduleItem
            {
                Start = s.Date.ToDateTime(tr.Start),
                End = s.Date.ToDateTime(tr.End),
                Id = i++,
                Name = tr.Name,
            })).ToList();
        }
        private async Task GetEmployees()
        {
            Employees = (List<EmployeeDTO>)await _scheduleService.GetEmployees();
        }
        
        private async Task GetData()
        {
            await GetEmployees();
            if (Employees == null) _errorMessage = "No Employees could be found";
            else
            {
                SelectedEmployeeId = Employees.First().EmployeeId;
                await GetEmployeeSchedule(SelectedEmployeeId);
            }
        }
        public class ScheduleItem
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End {  get; set; }
        }











    }
}
