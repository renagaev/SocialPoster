using SixLabors.Fonts;

namespace SocialPoster.Services;

public enum FontName
{
    FiraSans,
    CaveatBold
}

public class FontProvider
{
    private Dictionary<FontName, Font> cache = new();

    public Font GetFont(FontName fontName)
    {
        if (cache.TryGetValue(fontName, out var font))
        {
            return font;
        }

        var fileName = fontName switch
        {
            FontName.FiraSans => "FiraSans-Regular.ttf",
            FontName.CaveatBold => "Caveat-Bold.ttf",
            _ => throw new ArgumentOutOfRangeException(nameof(fontName), fontName, null)
        };
        var fontCollection = new FontCollection();
        fontCollection.Add($"./Fonts/{fileName}");
        font = fontCollection.Get(fontCollection.Families.First().Name).CreateFont(10);
        cache[fontName] = font;
        return font;
    }
}