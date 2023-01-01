using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SocialPoster.Models;

namespace SocialPoster.Drawers;

public interface IDrawer
{
    Image DrawQuote(Image background, QuoteDto quoteDto, Font font);
}