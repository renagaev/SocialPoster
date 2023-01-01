using Microsoft.Extensions.Options;
using Quartz;
using SocialPoster.Drawers;
using SocialPoster.ImageProviders;
using SocialPoster.Instagram;
using SocialPoster.Models;
using SocialPoster.Services;

namespace SocialPoster.Jobs;

public class InstagramVerseJob : IJob
{
    private readonly FontProvider _fontProvider;
    private readonly RandomSquareImageProvider _squareImageProvider;
    private readonly QuoteService _quoteService;
    private readonly IInstagramProvider _instagramProvider;
    private readonly RandomWideImageProvider _randomWideImageProvider;
    private readonly VersesJobOptions _options;

    public InstagramVerseJob(FontProvider fontProvider, RandomSquareImageProvider squareImageProvider, IOptions<VersesJobOptions> options,
        QuoteService quoteService, IInstagramProvider instagramProvider, RandomWideImageProvider randomWideImageProvider)
    {
        _options = options.Value;
        _fontProvider = fontProvider;
        _squareImageProvider = squareImageProvider;
        _quoteService = quoteService;
        _instagramProvider = instagramProvider;
        _randomWideImageProvider = randomWideImageProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var quote = await _quoteService.GetUnpostedQuote(QuoteType.GenericBibleVerse);
        var uploader = await _instagramProvider.GetUploader(_options.Username);
        await UploadRnBiblePost(quote, uploader);
        await UploadRnBibleStories(quote, uploader);
    }

    private async Task UploadRnBibleStories(QuoteDto quote, IInstagramUploader uploader)
    {
        var font = _fontProvider.GetFont(FontName.FiraSans);
        var background = await _randomWideImageProvider.GetImage();
        var drawed = new StoriesCenterImageDrawer().DrawQuote(background, quote, font);
        await uploader.UploadStory(drawed, $"{quote.Text}\n{quote.Author}");
    }

    private async Task UploadRnBiblePost(QuoteDto quote, IInstagramUploader uploader)
    {
        var tags = _options.Tags;
        var font = _fontProvider.GetFont(FontName.FiraSans);
        var background = await _squareImageProvider.GetImage();
        var drawed = new PostCenterImageDrawer().DrawQuote(background, quote, font);
        await uploader.UploadPost(drawed,
            $"{quote.Text}\n{quote.Author}\n{string.Join("\n", Enumerable.Repeat(".", 6))}\n{tags}");
    }
}