using Microsoft.Extensions.Options;
using Quartz;
using SocialPoster.Drawers;
using SocialPoster.ImageProviders;
using SocialPoster.Instagram;
using SocialPoster.Models;
using SocialPoster.Services;
using SocialPoster.Storage;

namespace SocialPoster.Jobs;

public class QuoteJob : IJob
{
    private readonly FontProvider _fontProvider;
    private readonly PaperImageProvider _imageProvider;
    private readonly QuoteService _quoteService;
    private readonly IInstagramProvider _instagramProvider;
    private readonly QuotesJobOptions _options;

    public QuoteJob(FontProvider fontProvider, PaperImageProvider imageProvider, QuoteService quoteService,
        IOptions<QuotesJobOptions> options, IInstagramProvider instagramProvider)
    {
        _fontProvider = fontProvider;
        _imageProvider = imageProvider;
        _quoteService = quoteService;
        _instagramProvider = instagramProvider;
        _options = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var tags = $".\n.\n.\n.\n.\n.\n{_options.Tags}";
        var font = _fontProvider.GetFont(FontName.CaveatBold);
        var image = await _imageProvider.GetImage();
        var quote = await _quoteService.GetUnpostedQuote(QuoteType.ChristianQuote);
        var drawed = new QuotesDrawer().DrawQuote(image, quote, font, _options.Username);
        var api = await _instagramProvider.GetUploader(_options.Username);
        await api.UploadPost(drawed, $"{quote.Text}\n{quote.Author}\n{tags}");
    }
}