namespace ConsoleApp;

public class Post {
    public Post( int userId, int id, string title, string body ) {
        UserId = userId;
        Id = id;
        Title = title;
        Body = body;
    }

    public int UserId {get; set;}
    public int Id {get; set;}

    public string Title {get; set;}
    public string Body {get; set;}

    public override bool Equals(object? obj)
    {
        // Sprawdź, czy obiekt nie jest nullem i czy jest tego samego typu
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        // Rzutuj obj na typ Post
        Post other = (Post)obj;

        // Porównaj wszystkie pola
        return (UserId == other.UserId) && 
               (Id == other.Id) && 
               (Title == other.Title) && 
               (Body == other.Body);
    }

    public override int GetHashCode()
    {
        // Użyj wartości hashcode wszystkich pól, aby obliczyć hashcode dla obiektu
        return HashCode.Combine(UserId, Id, Title, Body);
    }
}