using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IProductApplicationService
    {
        public Task CreateNewProductAsync(NewProductDTO product);
    }
}
