using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SocialPoster.Models;

namespace SocialPoster.Drawers;

public class PostCenterImageDrawer : IDrawer
{
    public Image DrawQuote(Image background, QuoteDto quoteDto, Font font)
    {
        var (width, height) = background.Size();
        var center = (float)(width / 2.0);
        var box = (width * 0.8, height * 0.8);
        var (fontSize, wrapped) = FontSizeHelper.FindMultilineFontSize(font, quoteDto.Text, ((int, int))box);
        font = new Font(font, fontSize);


        return
            background.Clone(ctx =>
            {
                ctx.Brightness(0.6f);
                ctx.Contrast(0.9f);


                ctx.DrawText(new TextOptions(font)
                {
                    Origin = new PointF(center, (float)(height * 0.1)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                }, wrapped, Color.White);


                var authorSize = FontSizeHelper.FindSinglelineFontSize(font, quoteDto.Author,
                    ((int, int))(width * 0.75, height * 0.06));
                var authorFont = new Font(font, authorSize);
                var authorHeight = TextMeasurer.Measure(quoteDto.Author, new TextOptions(authorFont)).Height;

                ctx.DrawText(new TextOptions(authorFont)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Origin = new PointF(center, height - authorHeight),
                }, quoteDto.Author, Color.White);
            });
    }
}