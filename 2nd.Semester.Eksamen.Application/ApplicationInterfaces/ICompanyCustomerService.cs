using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ICompanyCustomerService
    {
        public Task CreateCompanyCustomerAsync(CompanyCustomerDTO companyCustomerDTO);
    }
}
