using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PaymentPages
{
    public partial class CreateDiscount
    {
        private bool IsCampaign { get; set; }
        private string _errorMessage = "";
        private bool IsLoyalty { get; set; }
        private bool OpenPopUp { get; set; } = false;
        private List<ProductItem> allProducts = new();
        private bool OpenConfirmation { get; set; } = false;
        private LoyaltyDiscountDTO LoyaltyDiscountDTO { get; set; } = new();
        private CampaignDiscountDTO CampaignDiscountDTO { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsCampaign = true;
            IsLoyalty = false;
            allProducts = (await _discountService.GetAllProductsAsync()).Select(p => new ProductItem() { Id = p.ProductId, Name = p.Name, Selected = false }).ToList();
        }

        public string GetCampaignButtonClass()
        {
            if (IsCampaign) return "customer-type-btn-clicked";
            return "customer-type-btn";
        }
        public string GetLoyaltyButtonClass()
        {
            if (IsLoyalty) return "customer-type-btn-clicked";
            return "customer-type-btn";
        }
        public void DiscountTypeButtonClicked()
        {
            IsLoyalty = !IsLoyalty;
            IsCampaign = !IsCampaign;
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                if (IsLoyalty)
                {
                    await _discountService.CreateNewLoyaltyDiscountAsync(LoyaltyDiscountDTO);
                }
                if (IsCampaign)
                {
                    CampaignDiscountDTO.ProductIds.AddRange(allProducts.Where(p => p.Selected == true).Select(p => p.Id).ToList());
                    await _discountService.CreateNewCampaignDiscountAsync(CampaignDiscountDTO);
                }
            }
            catch
            {
                _errorMessage = "Noget gik galt så din rabat blev ikke oprettet";
            }
            Navi.NavigateTo("/");
        }

        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool Selected { get; set; }
        }
    }
}
