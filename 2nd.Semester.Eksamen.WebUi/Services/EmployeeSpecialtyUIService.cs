using _2nd.Semester.Eksamen.Application.DTO;

namespace _2nd.Semester.Eksamen.WebUi.Services
{
    public class EmployeeSpecialtyService
    {
        public void AddSpecialty(IHasSpecialties input)
        {
            input.Specialties.Add(new SpecialtyItemBase
            {
                Id = Guid.NewGuid(),
                Value = ""
            });
        }

        public void RemoveSpecialty(IHasSpecialties input, Guid id)
        {
            var item = input.Specialties.FirstOrDefault(s => s.Id == id);
            if (item != null)
                input.Specialties.Remove(item);
        }
    }

}
