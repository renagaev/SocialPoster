using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SocialPoster.Models;

namespace SocialPoster.Drawers;

public class QuotesDrawer
{
    public Image DrawQuote(Image background, QuoteDto quoteDto, Font font, string username)
    {
        var (width, height) = background.Size();
        var center = (float)(width / 2.0);
        var box = (width * 0.75, height * 0.75);
        var (fontSize, wrapped) = FontSizeHelper.FindMultilineFontSize(font, quoteDto.Text, ((int, int))box);
        font = new Font(font, fontSize);
            
        return
            background.Clone(ctx =>
            {
                //ctx.Contrast(0.65f);
                ctx.DrawText(
                    new TextOptions(font)
                    {
                        LineSpacing = 0.8f,
                        Origin = new PointF((float)(width * 0.07), (float)(height * 0.1))
                    },
                    wrapped,
                    Color.Black
                );

                var author = $"— {quoteDto.Author}";
                var authorFontSize =
                    FontSizeHelper.FindSinglelineFontSize(font, author, ((int, int))(width * 0.5, 120));
                var authorFont = new Font(font, authorFontSize);
                ctx.DrawText(
                    new TextOptions(authorFont)
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Origin = new PointF((float)(width * 0.95), (float)(width * 0.8))
                    },
                    author, Color.Black
                );

                username = "@" + username;
                var markFont = new Font(font, 60);
                var markHeight = TextMeasurer.Measure(username, new TextOptions(markFont)).Height;
                ctx.DrawText(new TextOptions(markFont)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Origin = new PointF(center, height - markHeight - 10)
                    }, username, Color.Black
                );
            });
    }
}