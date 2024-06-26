using Microsoft.EntityFrameworkCore;
using WebApplication3;

var builder = WebApplication.CreateBuilder();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");//"Server=
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db)=> await db.Users.ToListAsync());

app.MapGet("/api/users/{id:int}", async(int id, ApplicationContext db) =>
{
    User? user= await db.Users.FirstOrDefaultAsync(x=>x.Id== id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    return Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    User? user = await db.Users.FirstOrDefaultAsync(x=>x.Id == id);
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("/api/users", async (User user, ApplicationContext db) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
{
    var user=await db.Users.FirstOrDefaultAsync(x=>x.Id==userData.Id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    user.Age=userData.Age;
    user.Name=userData.Name;
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();
