var builder = WebApplication.CreateBuilder(args);

var strConnection = "Server=localhost;User Id=sa;Password=chang3Me;Database=demo;trustServerCertificate=true";
builder.Services.AddScoped(_ => new SqlConnection(strConnection));
builder.Services.AddScoped<UserService>();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(p => p.AllowAnyOrigin());

//app.UseDelta(shouldExecute: context => context.Request.Path.ToString().Contains("users"));

var group = app.MapGroup("users").UseDelta();

group.MapGet("/", async (string username, UserService userService) =>
{
    var user = await userService.SearchByUsername(username);
    return Results.Ok(user);
});

group.MapGet("/{userId:guid}", async (Guid userId, UserService userService) =>
{
    var user = await userService.GetByIdAsync(userId);
    return user is null ? Results.NotFound() : Results.Ok(user);
});

app.Run();
