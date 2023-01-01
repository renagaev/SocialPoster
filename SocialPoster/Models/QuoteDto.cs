namespace SocialPoster.Models;

public enum QuoteType
{
    GenericBibleVerse = 1,
    PrayBibleVerse,
    ChristianQuote
}

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