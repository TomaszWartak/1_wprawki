struct NewPostDto {
    public NewPostDto( int userId, string title, string body ) {
        UserId = userId;
        Title = title;
        Body = body;
    }

    public int UserId {get;}
    public string Title {get;}
    public string Body {get;}
    
}