using MudBlazor;

namespace Rapido.Web.UI.Themes;

public sealed class PrimaryTheme : MudTheme
{
    public PrimaryTheme()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#000b4b",
            Secondary = "#0070E0",
            Tertiary = "#1e30f3",
            Background = "#f5f5f5"
        };
    }
}