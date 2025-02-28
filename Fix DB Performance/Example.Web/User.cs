namespace Example.Api;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string PictureUrl { get; set; }
    
    public string Phone { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}
