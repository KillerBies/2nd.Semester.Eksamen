using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.DTO.DiscountDTO.CampaignDTO;

public class CampaignInputDTO
{
    public string Name { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool AppliesToProduct { get; set; }
    public bool AppliesToTreatment { get; set; }
    public int NumberOfUses { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Description { get; set; }
    public List<Product> ProductsInCampaign { get; set; }
}