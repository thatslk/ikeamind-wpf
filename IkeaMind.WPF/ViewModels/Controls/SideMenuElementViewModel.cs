using IkeaMind.WPF.Enums;

namespace IkeaMind.WPF.ViewModels.Controls
{
    public class SideMenuElementViewModel
    {
        public string ImageSource { get; set; }
        public MenuElemetsEnum Screen { get; set; }

        public SideMenuElementViewModel(string imagePath, MenuElemetsEnum screen)
        {
            ImageSource = imagePath;
            Screen = screen;
        }
    }
}
