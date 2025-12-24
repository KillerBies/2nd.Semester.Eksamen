namespace Components.Shared
{
    public class DropdownItem
    {
        public string Text { get; set; } = "";
        public string Menu { get; set; } = "";
        public bool Open { get; set; } = false;
        public DropdownItem(string text, string menu)
        {
            Text = text;
            Menu = menu;
        }
    }
}
