using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SocialPoster.Models;

namespace SocialPoster.Drawers;

public class StoriesCenterImageDrawer : IDrawer
{
    public Image DrawQuote(Image background, QuoteDto quoteDto, Font font)
    {
        var (width, height) = background.Size();
        var center = (float)(width / 2.0);
        const double verticalPadding = 0.25;
        var box = (width * 0.8, height * (1 - verticalPadding * 2));
        var (fontSize, wrapped) = FontSizeHelper.FindMultilineFontSize(font, quoteDto.Text, ((int, int))box);
        font = new Font(font, fontSize);
        

        return
            background.Clone(ctx =>
            {
                ctx.Brightness(0.6f);
                ctx.Contrast(0.8f);
                
                var textHeight = TextMeasurer.Measure(wrapped, new TextOptions(font)).Height;
                var y = (height * verticalPadding) + (box.Item2 - textHeight) / 2;
                ctx.DrawText(new TextOptions(font)
                {
                    Origin = new PointF(center, (float)y),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,   
                }, wrapped, Color.White);


                var authorSize = FontSizeHelper.FindSinglelineFontSize(font, quoteDto.Author,
                    ((int, int))(width * 0.6, height * 0.06));
                var authorFont = new Font(font, authorSize);
                var authorHeight = TextMeasurer.Measure(quoteDto.Author, new TextOptions(authorFont)).Height;
                ctx.DrawText(new TextOptions(authorFont)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Origin = new PointF(center, height - authorHeight - 30),
                }, quoteDto.Author, Color.White);
            });
    }
}