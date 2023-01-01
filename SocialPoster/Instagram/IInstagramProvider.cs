namespace SocialPoster.Instagram;

public interface IInstagramProvider
{
    Task<IInstagramUploader> GetUploader(string username);
}