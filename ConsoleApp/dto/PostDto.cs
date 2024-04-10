namespace ConsoleApp;

public struct PostDto
{
    public PostDto( int userId, int id, string title, string body )
    {
        Id = id;
        UserId = userId;
        Title = title;
        Body = body;
    }
    
    public PostDto( int id )
    {
        Id = id; 
    }
    
    public int Id {get; }
    public int? UserId {get; set; }
    public string? Title {get; set; }
    public string? Body {get; set; }
}