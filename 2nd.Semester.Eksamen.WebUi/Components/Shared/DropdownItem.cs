namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public class DropdownItem
    {
        public string Text { get; set; } = "";
        public string Link { get; set; } = "";
        public DropdownItem(string text, string link)
        {
            Text = text;
            Link = link;
        }
    }
}
