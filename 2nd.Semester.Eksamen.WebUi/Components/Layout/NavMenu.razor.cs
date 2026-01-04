using Components.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace _2nd.Semester.Eksamen.WebUi.Components.Layout
{
    public partial class NavMenu
    {
        //a given list of dropdown items
        [Parameter] public List<DropdownItem> items { get; set; } = new List<DropdownItem>();

        //Controlls for the dropdown menu.
        private bool Open = false;

        private bool ShowProductsDropdown = false;
        private bool ShowEmployeeDropdown = false;
        private bool ShowCustomerDropdown = false;
        private bool ShowBookingDropdown = false;
        private bool ShowProductOptionsDropdown = false;
        private void ToggleDropdown() => Open = !Open;
        private bool CreateBookingIsVisible { get; set; } = false;
        public bool CreateProductIsVisible { get; set; } = false;
        private bool CreateTreatmentIsVisible { get; set; } = false;
        private bool CreateEmployeeIsVisible { get; set; } = false;
        private bool CreateCustomerIsVisible { get; set; } = false;
        private bool CreateDiscountIsVisible { get; set; } = false;
        private bool CreateCustomerForBookingIsVisible { get; set; } = false;
        private void CloseDropdown(DropdownItem item = null)
        {
            Open = false;
            if (item != null)
            {
                item.Open = true;
            }
        }
    }
}

