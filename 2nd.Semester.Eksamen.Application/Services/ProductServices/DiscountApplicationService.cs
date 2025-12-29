using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.ProductServices
{
    public class DiscountApplicationService : IDiscountApplicationService
    {
        private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;
        private readonly ICampaignRepository _campaignDiscountRepository;
        private readonly IProductRepository _productRepository;
        private DTO_to_Domain _domainAdapter;
        private Domain_to_DTO _dtoAdapter;
        public DiscountApplicationService(ILoyaltyDiscountRepository discountRepository, DTO_to_Domain dTO_To_Domain, ICampaignRepository campaignRepository, IProductRepository productRepository, Domain_to_DTO domain_To_DTO)
        {
            _loyaltyDiscountRepository = discountRepository;
            _domainAdapter = dTO_To_Domain;
            _campaignDiscountRepository = campaignRepository;
            _productRepository = productRepository;
            _dtoAdapter = domain_To_DTO;
        }
        public async Task CreateNewLoyaltyDiscountAsync(LoyaltyDiscountDTO discount)
        {
            try
            {
               await _loyaltyDiscountRepository.CreateNewAsync(_domainAdapter.DTOLoyaltyDiscountToDomain(discount));
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteLoyaltyDiscountAsync(LoyaltyDiscountDTO discount)
        {
            try
            {
                await _loyaltyDiscountRepository.CreateNewAsync(_domainAdapter.DTOLoyaltyDiscountToDomain(discount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateLoyaltyDiscountAsync(LoyaltyDiscountDTO discount)
        {
            try
            {
                var dis = new LoyaltyDiscount(discount.MinimumVisits, discount.DiscountType, discount.Name, discount.TreatmentDiscount / 100, discount.ProductDiscount / 100) { AppliesToProduct = discount.AppliesToProduct, AppliesToTreatment = discount.AppliesToTreatment, Id = discount.Id, IsLoyalty = true };
                await _loyaltyDiscountRepository.UpdateAsync(dis);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public async Task CreateNewCampaignDiscountAsync(CampaignDiscountDTO discount)
        {
            try
            {
                await _campaignDiscountRepository.CreateNewAsync(_domainAdapter.DTOCampaignDiscountToDomain(discount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteCampaignDiscountAsync(CampaignDiscountDTO discount)
        {
            try
            {
                await _campaignDiscountRepository.CreateNewAsync(_domainAdapter.DTOCampaignDiscountToDomain(discount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateCampaignDiscountAsync(CampaignDiscountDTO discount)
        {
            try
            {
                List<Product> products = new();
                foreach(int id in discount.ProductIds)
                {
                    var product = await _productRepository.GetByIDAsync(id);
                    products.Add(product);
                }
                var dis = new Campaign(discount.Name, discount.TreatmentDiscount/100, discount.ProductDiscount/100, discount.Start, discount.End) {AppliesToProduct = discount.AppliesToProduct, AppliesToTreatment = discount.AppliesToTreatment, Id = discount.Id, IsLoyalty = false, ProductsInCampaign = products, Description = discount.Description};
                await _campaignDiscountRepository.UpdateAsync(dis);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return (await _productRepository.GetAllAsync()).Select(p => _dtoAdapter.ProductToDTO(p)).ToList();
        }

        public async Task<CampaignDiscountDTO> GetCampaignDiscountByIdAsync(int id)
        {
            var camp = (await _campaignDiscountRepository.GetByIDAsync(id));
            return new CampaignDiscountDTO(camp);
        }
        public async Task<LoyaltyDiscountDTO> GetLoyaltyDiscountByIdAsync(int id)
        {
            var loyal = (await _loyaltyDiscountRepository.GetByIDAsync(id));
            return new LoyaltyDiscountDTO(loyal);
        }
    }
}