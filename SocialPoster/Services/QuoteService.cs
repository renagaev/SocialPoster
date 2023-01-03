using Microsoft.EntityFrameworkCore;
using SocialPoster.Models;
using SocialPoster.Storage;

namespace SocialPoster.Services;

public class QuoteService
{
    private readonly AppDbContext _dbContext;

    public QuoteService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QuoteDto> GetUnpostedQuote(QuoteType type)
    {
        var entity = await _dbContext.Set<QuoteEntity>().AsQueryable()
            .Where(x => x.Type == type)
            .OrderBy(x => EF.Functions.Random())
            .FirstAsync();
        return new QuoteDto(entity.Text, entity.Author);
    }
}