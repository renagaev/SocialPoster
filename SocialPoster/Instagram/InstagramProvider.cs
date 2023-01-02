using Microsoft.EntityFrameworkCore;
using SocialPoster.Storage;

namespace SocialPoster.Instagram;

public class InstagramProvider : IInstagramProvider
{
    private readonly InstagramClient _instagramClient;
    private readonly AppDbContext _dbContext;

    public InstagramProvider(InstagramClient instagramClient, AppDbContext dbContext)
    {
        _instagramClient = instagramClient;
        _dbContext = dbContext;
    }

    public async Task<IInstagramUploader> GetUploader(string username)
    {
        var account = await _dbContext.Set<InstagramAccount>().FirstOrDefaultAsync(x => x.Username == username);
        if (account == null)
            throw new Exception("account not found");

        await _instagramClient.SetSettings("", account.Settings);
        return new InstagramUploader(_instagramClient, account.SessionId);
    }
}