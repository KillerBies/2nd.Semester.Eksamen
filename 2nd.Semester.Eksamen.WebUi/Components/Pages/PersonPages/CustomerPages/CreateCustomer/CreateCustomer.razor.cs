using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.CustomerPages.CreateCustomer
{
    public partial class CreateCustomer
    {
        private bool IsPrivate { get; set; } = true;
        private bool IsCompany { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsPrivate = true;
            IsCompany = false;
        }

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