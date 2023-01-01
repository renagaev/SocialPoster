using System.Data;
using Dapper;
using SocialPoster.Models;

namespace SocialPoster.Services;

public class QuoteService
{
    private readonly IDbConnection _connection;

    public QuoteService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<QuoteDto> GetUnpostedQuote(QuoteType type)
    {
        var res = await _connection.QueryFirstAsync<Quote>("select * from quotes where type = @type and not posted order by random()",
            new {type});
        // await _connection.ExecuteAsync("update quotes set posted = true where id = @id", new {id = res.Id});
        return new QuoteDto(res.Text, res.Author);
    }
}

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public QuoteType Type { get; set; }
    public bool Posted { get; set; }
}