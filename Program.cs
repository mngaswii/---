using Microsoft.EntityFrameworkCore;
using сайт_курсач.Data;
using сайт_курсач.Models;
using сайт_курсач.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    string[] roleNames = { "Admin", "Accountant", "Master", "Client", "Manager" };
    foreach (var roleName in roleNames)
    {
        if (!db.Roles.Any(r => r.Name == roleName))
            db.Roles.Add(new Role { Name = roleName });
    }
    db.SaveChanges();

    var adminRole = db.Roles.First(r => r.Name == "Admin");
    if (!db.Users.Any(u => u.Login == "admin"))
    {
        db.Users.Add(new User
        {
            Login = "admin",
            Password = PasswordHasher.Hash("admin123"),
            Email = "admin@loftbeauty.local",
            RoleId = adminRole.Id
        });
        db.SaveChanges();
    }
}

app.Run();
