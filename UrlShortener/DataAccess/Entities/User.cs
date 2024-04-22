using UrlShortener.DataAccess.Entities.Enums;

namespace UrlShortener.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    
    public virtual ICollection<Url> Urls { get; set; }
}