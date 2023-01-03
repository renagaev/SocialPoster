namespace SocialPoster.Models;

public class QuoteDto
{
    public string Text { get; }
    public string Author { get; }

    public QuoteDto(string text, string author)
    {
        Text = text;
        Author = author;
    }
}