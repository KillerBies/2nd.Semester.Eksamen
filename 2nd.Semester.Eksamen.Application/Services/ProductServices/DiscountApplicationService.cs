using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
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
                await _loyaltyDiscountRepository.CreateNewAsync(_domainAdapter.DTOLoyaltyDiscountToDomain(discount));
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
                await _campaignDiscountRepository.CreateNewAsync(_domainAdapter.DTOCampaignDiscountToDomain(discount));
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
    }
}