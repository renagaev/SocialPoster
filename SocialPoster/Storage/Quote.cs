namespace SocialPoster.Storage;

public class QuoteEntity
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public QuoteType Type { get; set; }
    public bool Posted { get; set; }
}