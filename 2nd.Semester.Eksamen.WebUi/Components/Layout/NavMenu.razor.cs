using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.WebUi.Components.Layout
{
    public partial class NavMenu
    {
        //a given list of dropdown items
        [Parameter] public List<DropdownItem> items { get; set; } = new List<DropdownItem>();

        //Controlls for the dropdown menu.
        private bool Open = false;
        private void ToggleDropdown() => Open = !Open;
        private void CloseDropdown() => Open = false;
    }
}

