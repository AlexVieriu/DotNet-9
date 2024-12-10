
using System.Data;
using Bogus;
using Dapper;
using Microsoft.Data.SqlClient;

var faker = new Faker<User>()
    .RuleFor(x => x.Id, Guid.CreateVersion7)
    .RuleFor(x => x.FullName, f => f.Person.FullName)
    .RuleFor(x => x.Email, f => f.Person.Email)
    .RuleFor(x => x.UserName, f => f.Person.UserName)
    .RuleFor(x => x.PictureUrl, f => f.Image.LoremFlickrUrl())
    .RuleFor(x => x.Phone, f => f.Person.Phone)
    .RuleFor(x => x.DateOfBirth, f => f.Person.DateOfBirth);

var users = faker.Generate(10_000_00);

var conString = "Server=localhost;User Id=sa;Password=chang3Me;Database=demo;trustServerCertificate=true";
var dbConnection = new SqlConnection(conString);
await dbConnection.OpenAsync();

foreach (var user in users)
{
    await dbConnection.ExecuteAsync(
        """
        insert into dbo.users (id, fullname, email, username, pictureurl, phone, dateofbirth) values
        (@Id, @FullName, @Email, @UserName, @PictureUrl, @Phone, @DateOfBirth)
        """, user);
}

await dbConnection.DisposeAsync();

class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string PictureUrl { get; set; }
    
    public string Phone { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public DataRowVersion RowVersion { get; set; }
}
