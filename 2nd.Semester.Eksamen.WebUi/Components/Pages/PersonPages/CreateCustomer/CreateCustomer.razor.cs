namespace Components.Pages.PersonPages.CreateCustomer
{
    public partial class CreateCustomer
    {
        private bool IsPrivate { get; set; } = true;
        private bool IsCompany { get; set; } = false;

        public string GetPrivateCustomerButtonClass()
        {
            if (IsPrivate) return "customer-type-btn-clicked";
            return "customer-type-btn";
        }
        public string GetCompanyCustomerButtonClass()
        {
            if (IsCompany) return "customer-type-btn-clicked";
            return "customer-type-btn";
        }
        public void CustomerTypeButtonClicked()
        {
            IsPrivate = !IsPrivate;
            IsCompany = !IsCompany;
        }
    }
}