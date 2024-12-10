namespace Example.Api;

public class UserService
{
    private readonly SqlConnection _sqlConnection;

    public UserService(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<User>(
            """
            select id, fullname, email, username, pictureurl, phone, dateofbirth from dbo.users where email = @email
            """,
            new { email });
    }
    
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<User>(
            """
            select id, fullname, email, username, pictureurl, phone, dateofbirth from dbo.users where id = @userId
            """,
            new { userId });
    }
    
    public async Task<IEnumerable<User?>> SearchByUsername(string username)
    {
        return await _sqlConnection.QueryAsync<User>(
            """
            select id, fullname, email, username, pictureurl, phone, dateofbirth from dbo.users where username like @username
            """,
            new { username = $"%{username}%" });
    }
}
